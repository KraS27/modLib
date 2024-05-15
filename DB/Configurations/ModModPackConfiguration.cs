using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.DB.Relationships;

namespace modLib.DB.Configurations
{
    public class ModModPackConfiguration : IEntityTypeConfiguration<ModModPack>
    {
        public void Configure(EntityTypeBuilder<ModModPack> builder)
        {
            builder.ToTable("mod_modPack");

            builder.HasKey(e => new { e.ModId, e.ModPackId });

            builder.HasOne(mod => mod.Mod)
                .WithMany(modPack => modPack.ModModPacks)
                .HasForeignKey(mod => mod.ModId);

            builder.HasOne(modPack => modPack.ModPack)
                .WithMany(mod => mod.ModModPacks)
                .HasForeignKey(modPack => modPack.ModPackId);
        }
    }
}
