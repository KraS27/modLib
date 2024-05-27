using modLib.Models.Entities;

namespace modLib.Entities.DbModels
{
    public class UserModel : BaseModel
    {
        public string? UserName { get; set; }

        public string Email {  get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}
