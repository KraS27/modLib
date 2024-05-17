namespace modLib.Entities.DTO.ModPacks
{
    public class GetModPacksDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Game { get; set; } = string.Empty;

        public int ModsCount { get; set; }
    }
}
