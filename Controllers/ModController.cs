using Microsoft.AspNetCore.Mvc;
using modLib.Models.Entities;
using modLib.Repositories;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController
    {
        public AppDbContext _context;

        public ModController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("mods")]
        public IActionResult GetMods()
        {
            var mods = new List<ModModel>
            {
                new ModModel {Name="ffafa", Description= "fasfasg",Path="http://5261fas51"},
                new ModModel {Name="asfgva", Description= "gasgasa",Path="http://587472gsagweg"},
            };

            _context.Mods.AddRange(mods);

            _context.SaveChanges();

            var modsDb = _context.Mods.ToList();
            return new OkObjectResult(modsDb);
        }
    }
}
