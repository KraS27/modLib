using modLib.Models.Entities;

namespace modLib.Entities.DbModels
{
    public class PermissionModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<RoleModel> Roles { get; set; } = new List<RoleModel>();
    }
}