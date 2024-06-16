using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using modLib.BL;
using modLib.DB;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Enums;
using modLib.Entities.Extensions;
using modLib.Handlers;
using System.Text;
using System.Text.Json.Serialization;

namespace modLib
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();           
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            builder.Services.Configure<Entities.DTO.Auth.AuthorizationOptions>(builder.Configuration.GetSection(nameof(Entities.DTO.Auth.AuthorizationOptions)));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = builder.Configuration["Jwt:Key"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jwt"];

                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => 
                policy.AddRequirements(new PermissionRequirement(new List<Permission> {Permission.Create})));
            });

            builder.Services.AddValidators();

            builder.Services.AddScoped<ModService>();
            builder.Services.AddScoped<ModPackService>();
            builder.Services.AddScoped<GameService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}