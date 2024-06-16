using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities.DTO.Game;
using modLib.Entities.Enums;

namespace modLib.BL
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HashSet<Permission>> GetPermissionsAsync(int id)
        {
            var roles = await _context.Users
                    .AsNoTracking()
                    .Include(u => u.Roles)
                    .ThenInclude(r => r.Permissions)
                    .Where(u => u.Id == id)
                    .Select(u => u.Roles)
                    .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
    }
}
