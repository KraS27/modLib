namespace modLib.Entities.DTO.Game
{
    public class GetGamesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Version { get; set; }

        public int ModPacksCount { get; set; }
    }
}
