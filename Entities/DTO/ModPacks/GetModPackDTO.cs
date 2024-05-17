namespace modLib.Entities.DTO.ModPacks
{
    public class GetModPackDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Game { get; set; } = string.Empty;

        public List<string> ModNames { get; set; } = new();

        public int ModCount { get; set; }
    }
}
