using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.DTO.ModPacks;
using modLib.Entities.Exceptions;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModPackService : BaseService<ModPackModel>
    {
        public ModPackService(AppDbContext context) : base(context) { }

        public async Task CreateAsync(CreateModPackDTO createModel)
        {
            var modPack = await _context.Mods.FirstOrDefaultAsync(m => m.Name == createModel.Name);
            var game = await _context.Games.FindAsync(createModel.GameId);

            if (modPack != null)
                throw new AlreadyExistException("ModPack with that name already exist");

            if (game == null)
                throw new ForeignKeyException($"Game with id: {createModel.GameId} not foud");

            var newModPack = new ModPackModel
            {
                Name = createModel.Name,
                Description = createModel.Description,
                GameId = game.Id,
            };

            await _context.AddAsync(newModPack);
            await _context.SaveChangesAsync();
        }
    }
}
