using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Enums;

namespace modLib.DB.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionDTO>
    {
        private readonly AuthorizationOptions _authOptions;

        public RolePermissionConfiguration(AuthorizationOptions authOptions)
        {
            _authOptions = authOptions;
        }

        public void Configure(EntityTypeBuilder<RolePermissionDTO> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(ParseRolePermissions());
        }

        private RolePermissionDTO[] ParseRolePermissions()
        {
            return _authOptions.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionDTO
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })).ToArray();
        }
    }
}
