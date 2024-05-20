using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modLib.BL;
using modLib.DB.Relationships;
using modLib.Entities.DTO.ModPacks;
using modLib.Entities.Exceptions;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.Controllers
{
    [ApiController]
    public class ModPackController : ControllerBase
    {
        private readonly ModPackService _service;
        private readonly ILogger<ModController> _logger;

        public ModPackController(ModPackService service, 
            ILogger<ModController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("modPacks/{id}")]
        public async Task<IActionResult> GetModPack(int id)
        {
            try
            {
                var mod = await _service.GetDTOAsync(id);

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
        public async Task<IActionResult> GetModPacks()
        {
            try
            {
                var mods = await _service.GetAllDTOAsync();

                return Ok(mods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting modPacks.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("modPacks")]
        public async Task<IActionResult> AddModPack([FromBody] CreateModPackDTO modPackDTO)
        {
            try
            {
                await _service.CreateAsync(modPackDTO);

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
        public async Task<IActionResult> UpdateModPack([FromBody] ModPackModel modPackModel)
        {
            try
            {
                await _service.UpdateAsync(modPackModel);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
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
            catch(ArgumentNullException ex)
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
    }
}
