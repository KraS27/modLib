using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using modLib.DB;
using modLib.Entities;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginModel.UserName);
            if (user == null)
                throw new NotFoundException($"User with UserName: {loginModel.UserName} NOT FOUND");

            if (PasswordHasher.Verify(loginModel.Password, user.Password))
                throw new LoginException("Wrong password");


        }

        private class JwtProvider
        {
            private readonly IConfiguration _config;

            public JwtProvider(IConfiguration config)
            {
                _config = config;
            }

            private string GenerateToken(UserModel user)
            {
                var key = _config["Jwt:Key"];
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddHours(2));

                var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenValue;
            }
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password) =>
                BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            public static bool Verify(string password, string hashPassword) =>
                BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
        }
    }
}
