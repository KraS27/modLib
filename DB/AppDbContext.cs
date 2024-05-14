using Microsoft.EntityFrameworkCore;
using modLib.Entities.Models;
using modLib.Models.Entities;

namespace modLib.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<ModModel> Mods { get; set; }

        DbSet<ModPackModel> ModPacks { get; set; }
    }
}
