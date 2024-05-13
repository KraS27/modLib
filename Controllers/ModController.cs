using Microsoft.AspNetCore.Mvc;
using modLib.BL;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController
    {       
        private readonly ModsService _repository;

        public ModController(ModsService repository)
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
