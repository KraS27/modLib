using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Models.Entities;

namespace modLib.DB.Configurations
{
    public class ModModelConfiguration : IEntityTypeConfiguration<ModModel>
    {
        public void Configure(EntityTypeBuilder<ModModel> builder)
        {
            builder.ToTable("mods");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Path).HasMaxLength(120);
        }
    }
}
