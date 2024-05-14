using modLib.DB;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModService : BaseService<ModModel>
    {
        public ModService(AppDbContext context) : base(context) { }
    }
}
