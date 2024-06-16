using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using modLib.DB.Configurations;
using modLib.DB.Relationships;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Models;
using modLib.Models.Entities;
using System.Reflection;

namespace modLib.DB
{
    public class AppDbContext : DbContext
    {
        private readonly IOptions<AuthorizationOptions> _authOptions;

        public AppDbContext(DbContextOptions<AppDbContext> options, 
            IOptions<AuthorizationOptions> authOptions) : base(options) 
        {
            _authOptions = authOptions;
        }  
        
        public DbSet<ModModel> Mods { get; set; }
        public DbSet<ModPackModel> ModPacks { get; set; }
        public DbSet<ModModPack> ModModPacks { get; set; }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions.Value));
        }
    }
}
