using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Game;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;

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

        public async Task CreateAsync(CreateGameDTO createModel)
        {
            var existGame = await _context.Games.FirstOrDefaultAsync(g => g.Name == createModel.Name);
            if (existGame != null)
                throw new AlreadyExistException($"Game with name: {createModel.Name} already exist");

            var game = new GameModel
            {
                Name = createModel.Name,
                Version = createModel.Version,
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateGameDTO updateModel)
        {
            var toUpdate = await _context.Games.FindAsync(updateModel.Id);
            if (toUpdate == null)
                throw new NotFoundException($"Game with id: {updateModel.Id} NOT FOUND");

            _context.Games.Entry(toUpdate).CurrentValues.SetValues(updateModel);
            await _context.SaveChangesAsync();
        }
    }
}
