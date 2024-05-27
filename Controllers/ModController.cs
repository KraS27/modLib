using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;
using modLib.Entities.Structs;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController : ControllerBase
    {       
        private readonly ModService _service;
        private readonly IValidator<CreateModDTO> _createModDTOValidator;
        private readonly IValidator<UpdateModDTO> _updateModDTOValidator;
        private readonly ILogger<ModController> _logger;

        public ModController(ModService repository,
            IValidator<CreateModDTO> createModDTOValidator,
            IValidator<UpdateModDTO> updateModDTOValidator,
            ILogger<ModController> logger)
        {
            _service = repository;
            _createModDTOValidator = createModDTOValidator;
            _updateModDTOValidator = updateModDTOValidator;
            _logger = logger;
        }

        [HttpGet("mods/{id}")]
        public async Task<IActionResult> GetMod(int id)
        {
            try
            {
                var mod = await _service.GetAsync(id);

                return mod is null ? NoContent() : Ok(mod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting mod.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("mods")]
        public async Task<IActionResult> GetMods([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var mods = await _service.GetAllAsync(new Pagination<GetModsDTO>(page, pageSize));

                return Ok(mods);
            }
            catch (PaginationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] CreateModDTO createModel)
        {
            var validationResult = await _createModDTOValidator.ValidateAsync(createModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {                                                
                await _service.CreateAsync(createModel);

                return Ok();
            }
            catch (AlreadyExistException ex )
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mod.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("mods/range")]
        public async Task<IActionResult> AddMods([FromBody] List<CreateModDTO> modsDTO)
        {
            var validationResults = modsDTO.Select(dto => _createModDTOValidator.Validate(dto)).ToList();

            var validationErrors = validationResults
                .Where(result => !result.IsValid)
                .SelectMany(result => result.Errors)
                .Select(error => new {Value = error.AttemptedValue, Error = error.ErrorMessage})
                .ToList();

            if (validationErrors.Any())            
                return BadRequest(validationErrors);
            
            try
            {                              
                await _service.CreateRangeAsync(modsDTO);
                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ForeignKeyException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("mods/{id}")]
        public async Task<IActionResult> RemoveMod(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return Ok();
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing mod.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("mods")]
        public async Task<IActionResult> UpdateMod([FromBody] UpdateModDTO updateModel)
        {
            try
            {
                var validationResult = await _updateModDTOValidator.ValidateAsync(updateModel);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

                await _service.UpdateAsync(updateModel);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating mod.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
