using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modLib.BL;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;
using modLib.Models.Entities;

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
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("mods")]
        public async Task<IActionResult> GetMods()
        {
            try
            {
                var mods = await _service.GetAllWithGamesAsync();

                return Ok(mods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] CreateModDTO modDTO)
        {
            var validationResult = await _createModDTOValidator.ValidateAsync(modDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {                                                
                await _service.CreateAsync(modDTO);

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
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
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
                .Select(error => error.ErrorMessage)
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
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("mods")]
        public async Task<IActionResult> UpdateMod([FromBody] UpdateModDTO modModel)
        {
            try
            {
                var validationResult = await _updateModDTOValidator.ValidateAsync(modModel);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

                await _service.UpdateDTOAsync(modModel);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mods.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
