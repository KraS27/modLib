using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Game;
using modLib.Entities.DTO.Mods;

namespace modLib.BL
{
    public class GameService : BaseService<GameModel>
    {
        public GameService(AppDbContext context) : base(context) { }

        public new async Task<GetGameDTO?> GetAsync(int id)
        {
            var game = await _context.Games
                .Select(g => new GetGameDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Version = g.Version,
                    ModPacks = g.ModPacks!.Select(m => m.Name).ToList(),
                    ModsCount = g.Mods!.Count
                })
                .FirstOrDefaultAsync(g => g.Id == id);
                
            return game;
        }

        public async Task<IEnumerable<GetGamesDTO>> GetAllAsync(Pagination<GetGamesDTO> pagination)
        {
            var games = _context.Games.Select(g => new GetGamesDTO
            {
                Id = g.Id,
                Name = g.Name,
                Version = g.Version,
                ModPacksCount = g.ModPacks!.Count
            }).OrderBy(m => m.Id).AsQueryable();

            return await pagination.Apply(games).ToListAsync();
        }
    }
}
