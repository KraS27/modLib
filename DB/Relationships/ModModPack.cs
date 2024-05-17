using modLib.Entities.Models;
using modLib.Models.Entities;
using System.Text.Json.Serialization;

namespace modLib.DB.Relationships
{
    public class ModModPack
    {
        public int ModId { get; set; }
        [JsonIgnore]
        public ModModel? Mod { get; set; }

        public int ModPackId { get; set; }
        [JsonIgnore]
        public ModPackModel? ModPack { get; set; }
    }
}
