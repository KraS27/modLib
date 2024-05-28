using Microsoft.EntityFrameworkCore;
using modLib.DB;
using modLib.Entities;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Exceptions;

namespace modLib.BL
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Register(RegisterModel registerModel)
        {
            var hashedPassword = PasswordHasher.HashPassword(registerModel.Password);

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == registerModel.UserName);
            if (existUser != null)
                throw new AlreadyExistException($"User with userName:{registerModel.UserName} already exist.");

            var user = new UserModel
            {
                UserName = registerModel.UserName,
                Password = hashedPassword
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
