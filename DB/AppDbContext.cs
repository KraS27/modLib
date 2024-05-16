using Microsoft.EntityFrameworkCore;
using modLib.DB.Relationships;
using modLib.Entities.DbModels;
using modLib.Entities.Models;
using modLib.Models.Entities;
using System.Reflection;

namespace modLib.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ModModel> Mods { get; set; }
        public DbSet<ModPackModel> ModPacks { get; set; }
        public DbSet<ModModPack> ModModPacks { get; set; }
        public DbSet<GameModel> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
