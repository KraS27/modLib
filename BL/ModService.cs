using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModService : BaseService<ModModel>
    {
        public ModService(AppDbContext context) : base(context) { }

        public async Task CreateAsync(CreateModDTO createModel)
        {
            var check = await _context.Mods.FirstOrDefaultAsync(m => m.Name == createModel.Name);

            if (check != null)
                throw new AlreadyExistException();

            var mod = new ModModel { Name = createModel.Name, Description = createModel.Description, Path = createModel.Path };

            await _context.Mods.AddAsync(mod);
            await _context.SaveChangesAsync();
        }
    }
}
