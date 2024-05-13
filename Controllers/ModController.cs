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
            var mod = await _service.GetAsync(id);

            return mod is null ? new NoContentResult() : new OkObjectResult(mod);
        }

        [HttpGet("mods")]
        public async Task<IActionResult> GetMods()
        {
            var mods = await _service.GetAllAsync();
          
            return new OkObjectResult(mods);        
        }

        [HttpPut("mods")]
        public async Task<IActionResult> AddMod(ModModel modModel)
        {
            await _service.CreateAsync(modModel);

            return new OkObjectResult(modModel.Id);
        }
    }
}
