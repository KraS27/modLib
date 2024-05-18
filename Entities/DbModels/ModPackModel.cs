using modLib.DB.Relationships;
using modLib.Entities.DbModels;
using modLib.Models.Entities;
using System.Text.Json.Serialization;

namespace modLib.Entities.Models
{
    public class ModPackModel : BaseModel
    {
        public string Name {  get; set; } = string.Empty;

        public string? Description { get; set; }

        public int GameId { get; set; }
      
        public GameModel? Game { get; set; }
        
        public List<ModModPack>? ModModPacks { get; set; }
    }
}
