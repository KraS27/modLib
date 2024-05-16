namespace modLib.Entities.DTO.Mods
{
    public class GetModDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Path { get; set; }

        public string Game { get; set; } = string.Empty;
    }
}
