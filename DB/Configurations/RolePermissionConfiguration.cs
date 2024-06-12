using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DTO.Auth;

namespace modLib.DB.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionDTO>
    {
        public void Configure(EntityTypeBuilder<RolePermissionDTO> builder)
        {
            
        }
    }
}
