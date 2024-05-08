using Microsoft.EntityFrameworkCore;
using modLib.Models.Entities;

namespace modLib.Repositories
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       public DbSet<ModModel> Mods { get; set; }
    }
}
