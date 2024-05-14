using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.Exceptions;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModService : BaseService<ModModel>
    {
        public ModService(AppDbContext context) : base(context) { }

        public async override Task CreateAsync(ModModel model)
        {
            var check = await _context.Mods.FirstOrDefaultAsync(x => x.Id == model.Id || x.Name.ToLower() == model.Name.ToLower());

            if (check != null)
                throw new AlreadyExistException();

            await _context.Mods.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
