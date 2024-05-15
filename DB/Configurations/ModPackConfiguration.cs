using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.Models;

namespace modLib.DB.Configurations
{
    public class ModPackConfiguration : IEntityTypeConfiguration<ModPackModel>
    {
        public void Configure(EntityTypeBuilder<ModPackModel> builder)
        {
            builder.ToTable("modPacks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().HasColumnName("id");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(64).HasColumnName("name");
            builder.Property(x => x.Description).HasColumnName("description");

            builder.HasOne(x => x.Game)
                .WithMany(x => x.ModPacks)
                .HasForeignKey(x => x.GameId);
        }
    }
}
