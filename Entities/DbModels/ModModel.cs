using modLib.DB.Relationships;
using modLib.Entities.DbModels;
using modLib.Entities.Models;
using System.Text.Json.Serialization;

namespace modLib.Models.Entities
{
    public class ModModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Path { get; set; }

        public int GameId { get; set; }
       
        public GameModel? Game { get; set; }
      
        public List<ModModPack>? ModModPacks { get; set; }
    }
}
