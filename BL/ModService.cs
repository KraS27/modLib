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
            if (mod != null)
                throw new AlreadyExistException("Mod with that name already exist");

            var game = await _context.Games.FindAsync(createModel.GameId);
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

        public new async Task<IEnumerable<GetModDTO>> GetAllAsync()
        {
            var mods = await _context.Mods.Select(m => new GetModDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Path = m.Path,
                Game = m.Game!.Name
            }).ToListAsync();

            return mods;
        }

        public async Task UpdateAsync(UpdateModDTO updateModel)
        {
            var toUpdate = await _context.Mods.FindAsync(updateModel.Id);

            if (toUpdate == null)
                throw new NotFoundException($"Mod with id: {updateModel.Id} NOT FOUND");

            _context.Entry(toUpdate).CurrentValues.SetValues(updateModel);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(List<CreateModDTO> createModels)
        {
            var modNames = createModels.Select(m => m.Name).ToList();
            var gameIds = createModels.Select(m => m.GameId).ToList();

            var existingMods = await _context.Mods
                .Where(m => modNames.Contains(m.Name))
                .ToListAsync();

            var existingGames = await _context.Games
                .Where(g => gameIds.Contains(g.Id))
                .Select(g => g.Id)
                .ToListAsync();

            if (existingMods.Any())
                throw new AlreadyExistException($"Mods with names: {String.Join(", ", existingMods.Select(x => x.Name))} already exist");

            foreach(var model in createModels)
            {
                if(!existingGames.Contains(model.GameId))
                    throw new ForeignKeyException($"Game with id: {model.GameId} not found");
            }

            var newMods = createModels.Select(m => new ModModel
            {
                Name = m.Name,
                Description = m.Description,
                Path = m.Path,
                GameId = m.GameId
            }).ToList();
         
            await _context.Mods.AddRangeAsync(newMods);
            await _context.SaveChangesAsync();
        }
    }
}
