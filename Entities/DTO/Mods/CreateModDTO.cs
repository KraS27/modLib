namespace modLib.Entities.DTO.Mods
{
    public class CreateModDTO
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Path { get; set; }

        public int GameId { get; set; }
    }
}
