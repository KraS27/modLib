using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using modLib.DB;
using modLib.Entities;
using modLib.Entities.Constants;
using modLib.Entities.DbModels;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Enums;
using modLib.Entities.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace modLib.BL
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task Register(RegisterModel registerModel)
        {
            var hashedPassword = PasswordHasher.HashPassword(registerModel.Password);

            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == registerModel.UserName);
            if (existUser != null)
                throw new AlreadyExistException($"User with userName:{registerModel.UserName} already exist.");

            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Rabotyaga) ?? throw new InvalidOperationException();

            var user = new UserModel
            {
                UserName = registerModel.UserName,
                Password = hashedPassword,
                Roles = new[] { roleEntity }
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == loginModel.UserName);
            if (user == null)
                throw new NotFoundException($"User with UserName: {loginModel.UserName} NOT FOUND");

            if (!PasswordHasher.Verify(loginModel.Password, user.Password))
                throw new InvalidPasswordException("Wrong password");

            var token = JwtProvider.GenerateToken(user, _config);

            return token;
        }

        public static class JwtProvider
        {
            public static string GenerateToken(UserModel user, IConfiguration config)
            {
                var key = config["Jwt:Key"];
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)), SecurityAlgorithms.HmacSha256);

                Claim[] claims = new[] { new Claim(CustomClaims.UserId, user.Id.ToString()) };

                var token = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
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
