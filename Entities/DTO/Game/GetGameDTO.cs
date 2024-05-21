using modLib.Entities.Models;

namespace modLib.Entities.DTO.Game
{
    public class GetGameDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Version { get; set; }

        public List<string> ModPacks { get; set; } = new List<string>();

        public int ModsCount { get; set; }
    }
}
