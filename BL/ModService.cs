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
            var mod = await _context.Mods.FirstOrDefaultAsync(m => m.Name == createModel.Name);
            var game = await _context.Games.FindAsync(createModel.GameId);

            if (mod != null)
                throw new AlreadyExistException("Mod with that name already exist");

            if(game == null)
                throw new ForeignKeyException($"Game with id: {createModel.GameId} not foud");

            var newMod = new ModModel 
            { 
                Name = createModel.Name,
                Description = createModel.Description,
                Path = createModel.Path,
                GameId = createModel.GameId
            };

            await _context.Mods.AddAsync(newMod);
            await _context.SaveChangesAsync();
        }
    }
}
