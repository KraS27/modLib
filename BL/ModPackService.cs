using modLib.DB;
using modLib.Entities.Models;

namespace modLib.BL
{
    public class ModPackService : BaseService<ModPackModel>
    {
        public ModPackService(AppDbContext context) : base(context) { }
    }
}
