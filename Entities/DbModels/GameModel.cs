using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.Entities.DbModels
{
    public class GameModel : BaseModel
    {
        public string Name { get; set; } = String.Empty;

        public string? Version {  get; set; }

        public List<ModModel>? Mods { get; set; }

        public List<ModPackModel>? ModPacks { get; set; }
    }
}
