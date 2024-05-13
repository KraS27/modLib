using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Models.Entities;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController
    {       
        private readonly ModsService _service;

        public ModController(ModsService repository)
        {
            _service = repository;
        }

        [HttpGet("mods/{id}")]
        public async Task<IActionResult> GetMod(Guid id)
        {
            try
            {
                var mod = await _service.GetAsync(id);

                return mod is null ? new NoContentResult() : new OkObjectResult(mod);
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

                return new OkObjectResult(mods);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }               
        }

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] ModModel modModel)
        {
            try
            {
                await _service.CreateAsync(modModel);

                return new OkObjectResult(modModel.Id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpDelete("mods/{id}")]
        public async Task<IActionResult> RemoveMod(Guid id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return new OkResult();
            }
            catch(ArgumentNullException)
            {
                return new NotFoundResult();
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

                return new OkResult();
            }
            catch (NullReferenceException)
            {
                return new NotFoundResult();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
