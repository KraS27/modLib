namespace modLib.Entities.DTO.ModPacks
{
    public class AddModsToModPackDTO
    {
        public List<int> ModsId { get; set; } = new List<int>();

        public int ModPackId { get; set; }
    }
}
