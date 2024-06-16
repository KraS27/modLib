using Microsoft.AspNetCore.Authorization;
using modLib.Entities.Enums;

namespace modLib.Entities.DTO.Auth
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public List<Permission> Permissions { get; set; }

        public PermissionRequirement(List<Permission> permissions)
        {
            Permissions = permissions;
        }
    }
}
