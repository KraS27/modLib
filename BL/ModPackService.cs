using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModPackService : BaseService<ModPackModel>
    {
        public ModPackService(AppDbContext context) : base(context) { }

        public override async Task<ModPackModel?> GetAsync(Guid id)
        {
            return await _context.ModPacks.Include(x => x.Mods).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
