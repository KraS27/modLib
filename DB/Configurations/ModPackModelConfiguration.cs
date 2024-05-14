using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.Models;

namespace modLib.DB.Configurations
{
    public class ModPackModelConfiguration : IEntityTypeConfiguration<ModPackModel>
    {
        public void Configure(EntityTypeBuilder<ModPackModel> builder)
        {
            builder.ToTable("mod_packs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasColumnName("id").HasMaxLength(64);
            builder.Property(x => x.Description).HasColumnName("description");

            builder.HasMany(x => x.Mods)
                .WithOne(x => x.ModPack)
                .HasForeignKey(x => x.ModPackId);
        }
    }
}
