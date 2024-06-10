using modLib.Models.Entities;

namespace modLib.Entities.DbModels
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<UserModel>? Users { get; set; }

        public ICollection<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
    }
}