using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.DB.Relationships
{
    public class ModModPack
    {
        public int ModId { get; set; }
        public ModModel Mod { get; set; } = new ModModel();

        public int ModPackId { get; set; }
        public ModPackModel ModPack { get; set; } = new ModPackModel();
    }
}
