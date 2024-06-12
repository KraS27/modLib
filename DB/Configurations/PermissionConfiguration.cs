using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DbModels;
using modLib.Entities.Enums;

namespace modLib.DB.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionModel>
    {
        public void Configure(EntityTypeBuilder<PermissionModel> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(x => x.Id);

            var permissions = Enum
                .GetValues<Permission>()
                .Select(p => new PermissionModel
                {
                    Id = (int)p,
                    Name = p.ToString(),
                });

            builder.HasData(permissions);
        }
    }
}
