using modLib.DB;
using modLib.Entities.DbModels;

namespace modLib.BL
{
    public class GameService : BaseService<GameModel>
    {
        public GameService(AppDbContext context) : base(context) { }
    }
}
