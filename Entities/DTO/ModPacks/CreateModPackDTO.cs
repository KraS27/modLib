namespace modLib.Entities.DTO.ModPacks
{
    public class CreateModPackDTO
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int GameId { get; set; }
    }
}
