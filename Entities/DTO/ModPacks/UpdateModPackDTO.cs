namespace modLib.Entities.DTO.ModPacks
{
    public class UpdateModPackDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
