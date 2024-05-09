using modLib.Models.Entities;

namespace modLib.Repositories
{
    public class ModsRepository : BaseRepository<ModModel>, IBaseRepository<ModModel>
    {
        public ModsRepository(AppDbContext context) : base(context) { }
    }
}
