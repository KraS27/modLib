namespace modLib.Entities.DTO.Auth
{
    public class AuthorizationOptions
    {
        public List<RolePermissionDTO> RolePermissions { get; set; } = new List<RolePermissionDTO>();
    }
}
