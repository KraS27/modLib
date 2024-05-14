using modLib.Models.Entities;

namespace modLib.Entities.Models
{
    public class ModPackModel : BaseModel
    {
        public string Name {  get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<ModModel> Mods { get; set; } = new List<ModModel>();
    }
}
