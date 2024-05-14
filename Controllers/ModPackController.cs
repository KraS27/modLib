using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modLib.BL;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.Controllers
{
    [ApiController]
    public class ModPackController : ControllerBase
    {
        private readonly ModPackService _service;

        public ModPackController(ModPackService repository)
        {
            _service = repository;
        }

        [HttpGet("modPacks/{id}")]
        public async Task<IActionResult> GetModPack(Guid id)
        {
            try
            {
                var mod = await _service.GetAsync(id);

                return mod is null ? NoContent() : Ok(mod);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("modPacks")]
        public async Task<IActionResult> GetModPacks()
        {
            try
            {
                var mods = await _service.GetAllAsync();

                return Ok(mods);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("modPacks")]
        public async Task<IActionResult> AddModPack([FromBody] ModPackModel modPackModel)
        {
            try
            {
                await _service.CreateAsync(modPackModel);

                return Ok(modPackModel.Id);
            }
            catch (DbUpdateException)
            {
                return BadRequest("ModPack with that name already exist");
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("modPacks/{id}")]
        public async Task<IActionResult> RemoveModPack(Guid id)
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
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
