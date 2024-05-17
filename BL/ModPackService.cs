using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.DB.Relationships;
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

            await _context.ModPacks.AddAsync(newModPack);
            await _context.SaveChangesAsync();
        }

        public async Task AddModToModPack(int modPackId, int modId)
        {
            var mod = await _context.Mods.FindAsync(modId);
            var modPack = await _context.ModPacks.FindAsync(modPackId);

            if (mod == null)
                throw new ArgumentNullException($"Mod with id: {modId} Not Found");
            if (modPack == null)
                throw new ArgumentNullException($"ModPack with id: {modPackId} Not Found");

            var newRelation = new ModModPack { ModId = modId, ModPackId =  modPackId };

            var relation = await _context.ModModPacks.FirstOrDefaultAsync(m => m.ModPackId == modPackId && m.ModId == modId);
            if (relation != null)
                throw new AlreadyExistException($"Mod with id: {modId} already added to modPack with id: {modPackId}");

            await _context.ModModPacks.AddAsync(newRelation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetModPacksDTO>> GetAllDTOAsync()
        {
            var modPacks = await _context.ModPacks.Select(m => new GetModPacksDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Game = m.Game!.Name,
                ModsCount = m.ModModPacks!.Count()
            }) .ToListAsync();

            return modPacks;
        }
        
        public async Task<GetModPackDTO> GetDTOAsync(int id)
        {
            var modPackDTO = await _context.ModPacks
                .Where(m => m.Id == id)
                .Select(mp => new GetModPackDTO
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    Game = mp.Game!.Name,
                    ModCount = mp.ModModPacks!.Count(),
                    ModNames = mp.ModModPacks!.Select(x => x.Mod!.Name).ToList()
                }).FirstOrDefaultAsync();

            if (modPackDTO == null)
                throw new NotFoundException($"modPack with id: {id} Not Found");

            return modPackDTO;
        }
    }
}
