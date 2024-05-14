using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.Models;

namespace modLib.DB.Configurations
{
    public class ModPackModelConfiguration : IEntityTypeConfiguration<ModPackModel>
    {
        public void Configure(EntityTypeBuilder<ModPackModel> builder)
        {
            builder.ToTable("modPacks");

            builder.HasKey(x => x.Id);
           
            builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Description).HasColumnName("description");                        
        }
    }
}
