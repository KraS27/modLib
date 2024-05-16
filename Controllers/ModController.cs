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

        public ModController(ModService repository)
        {
            _service = repository;
        }

        [HttpGet("mods/{id}")]
        public async Task<IActionResult> GetMod(int id)
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

        [HttpGet("mods")]
        public async Task<IActionResult> GetMods()
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

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] CreateModDTO modDTO)
        {
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
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }         
        }

        [HttpPut("mods")]
        public async Task<IActionResult> UpdateMod([FromBody] ModModel modModel)
        {
            try
            {
                await _service.UpdateAsync(modModel);

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
