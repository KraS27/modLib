using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Game;

namespace modLib.BL
{
    public class GameService : BaseService<GameModel>
    {
        public GameService(AppDbContext context) : base(context) { }

        public new async Task<GetGameDTO> GetAsync(int id)
        {
            var game = await _context.Games
                .Select(g => new GetGameDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Version = g.Version,
                    ModPacks = g.ModPacks!.Select(m => m.Name).ToList(),
                    ModsCount = g.Mods.Count
                })
                .FirstOrDefaultAsync(g => g.Id == id);
                
            return game;
        }
    }
}
