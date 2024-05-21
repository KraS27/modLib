using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.DB.Relationships;
using modLib.Entities;
using modLib.Entities.DTO.ModPacks;
using modLib.Entities.Exceptions;

namespace modLib.Controllers
{
    [ApiController]
    public class ModPackController : ControllerBase
    {
        private readonly ModPackService _service;
        private readonly ILogger<ModController> _logger;
        private readonly IValidator<CreateModPackDTO> _createModPackvalidator;
        private readonly IValidator<UpdateModPackDTO> _updateModPackvalidator;

        public ModPackController(ModPackService service,
            ILogger<ModController> logger,
            IValidator<CreateModPackDTO> createModPackvalidator,
            IValidator<UpdateModPackDTO> updateModPackvalidator)
        {
            _service = service;
            _logger = logger;
            _createModPackvalidator = createModPackvalidator;
            _updateModPackvalidator = updateModPackvalidator;
        }

        [HttpGet("modPacks/{id}")]
        public async Task<IActionResult> GetModPack(int id)
        {
            try
            {
                var mod = await _service.GetAsync(id);

                return Ok(mod);
            }
            catch(NotFoundException ex)
            { 
                return Ok(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("modPacks")]
        public async Task<IActionResult> GetModPacks([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var mods = await _service.GetAllAsync(new Pagination<GetModPacksDTO>(page, pageSize));

                return Ok(mods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting modPacks.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("modPacks")]
        public async Task<IActionResult> AddModPack([FromBody] CreateModPackDTO createModel)
        {
            var validationResult = _createModPackvalidator.Validate(createModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _service.CreateAsync(createModel);

                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("modPacks/{id}")]
        public async Task<IActionResult> RemoveModPack(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("modPacks")]
        public async Task<IActionResult> UpdateModPack([FromBody] UpdateModPackDTO updateModel)
        {
            var validationResult = _updateModPackvalidator.Validate(updateModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _service.UpdateAsync(updateModel);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("modPacks/addMod")]
        public async Task<IActionResult> AddModToModPack([FromBody] ModModPack relation)
        {
            try
            {
                await _service.AddModToModPack(relation.ModPackId, relation.ModId);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mod to modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("modPacks/addMods")]
        public async Task<IActionResult> AddModsToModPack([FromBody] AddModsToModPackDTO relations)
        {
            try
            {
                await _service.AddModsToModPack(relations);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding mod to modPack.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
