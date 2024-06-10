using modLib.Models.Entities;

namespace modLib.Entities.DbModels
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; } = string.Empty;

        public string? Email {  get; set; }

        public string Password { get; set; } = string.Empty;

        public int Age { get; set; }

        public ICollection<RoleModel> Roles { get; set; } = new List<RoleModel>();
    }
}
