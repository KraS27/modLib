using modLib.DB;
using modLib.Models.Entities;

namespace modLib.BL
{
    public class ModsService : BaseService<ModModel>, IBaseService<ModModel>
    {
        public ModsService(AppDbContext context) : base(context) { }
    }
}
