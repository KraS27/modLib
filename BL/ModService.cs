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

        public async Task<IEnumerable<GetModDTO>> GetAllWithGamesAsync()
        {
            var mods = await _context.Mods.Include(m => m.Game).ToListAsync();

            var modsGetDTO = mods.Select(m => new GetModDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Path = m.Path,
                Game = m.Game!.Name
            });

            return modsGetDTO;
        }

        public async Task UpdateDTOAsync(UpdateModDTO updateDTO)
        {
            var toUpdate = await _context.Mods.FindAsync(updateDTO.Id);

            if (toUpdate == null)
                throw new NotFoundException($"Mod with id: {updateDTO.Id} NOT FOUND");

            _context.Entry(toUpdate).CurrentValues.SetValues(updateDTO);
            await _context.SaveChangesAsync();
        }
    }
}
