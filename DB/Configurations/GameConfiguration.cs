using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using modLib.Entities.DbModels;

namespace modLib.DB.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<GameModel>
    {
        public void Configure(EntityTypeBuilder<GameModel> builder)
        {
            builder.ToTable("games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().HasColumnName("id");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(64).HasColumnName("name");
            builder.Property(x => x.Version).HasColumnName("version");

            builder.HasMany(x => x.Mods)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            builder.HasMany(x => x.ModPacks)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);
        }
    }
}
