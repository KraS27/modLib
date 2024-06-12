using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;

namespace modLib.DB.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().HasColumnName("id");
            builder.Property(x => x.UserName).IsRequired().HasColumnName("userName");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Password).IsRequired().HasColumnName("password");
            builder.Property(x => x.Age).HasColumnName("age");

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleDTO>(                
                l => l.HasOne<RoleModel>().WithMany().HasForeignKey(r => r.RoleId),
                r => r.HasOne<UserModel>().WithMany().HasForeignKey(u => u.UserId),
                t => t.ToTable("user_roles"));
        }
    }
}
