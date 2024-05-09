using Microsoft.AspNetCore.Mvc;
using modLib.Models.Entities;
using modLib.Repositories;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController
    {       
        private readonly ModsRepository _repository;

        public ModController(ModsRepository repository)
        {
            _repository = repository;
        }
     
        [HttpGet("mods")]
        public IActionResult GetMods()
        {
            var mods = _repository.GetAllAsync().Result;

            return new OkObjectResult(mods);
        }
    }
}
