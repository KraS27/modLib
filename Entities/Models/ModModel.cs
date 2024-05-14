using modLib.Entities.Models;

namespace modLib.Models.Entities
{
    public class ModModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Path { get; set; }

        public int ModPackId { get; set; }
        public ModPackModel? ModPack {  get; set; }
    }
}
