using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Enums;

namespace modLib.DB.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().HasColumnName("id");
            builder.Property(x => x.Name).IsRequired().HasColumnName("name");

            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermissionDTO>(
                l => l.HasOne<PermissionModel>().WithMany().HasForeignKey(r => r.PermissionId),
                r => r.HasOne<RoleModel>().WithMany().HasForeignKey(r => r.RoleId));

            var roles = Enum
                .GetValues<Role>()
                .Select(e => new RoleModel
                {
                    Id = (int)e,
                    Name = e.ToString()
                });

            builder.HasData(roles);
        }
    }
}
