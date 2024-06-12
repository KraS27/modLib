namespace modLib.Entities.DTO.Auth
{
    public class AuthorizationOptions
    {
        public List<RolePermissions> RolePermissions { get; set; } = new List<RolePermissions>();
    }

    public class RolePermissions
    {
        public string Role {  get; set; } = string.Empty;

        public List<string> Permissions { get; set; } = new List<string>();
    }
}
