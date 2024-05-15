using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModPackService : BaseService<ModPackModel>
    {
        public ModPackService(AppDbContext context) : base(context) { }

        public async Task<ModPackModel?> GetWithModsAsync(int id)
        {
            return await _context.ModPacks.Include(x => x.ModModPacks).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
