using FluentValidation;
using Microsoft.EntityFrameworkCore;
using modLib.BL;
using modLib.DB;
using modLib.Entities.DTO.Game;
using modLib.Entities.DTO.ModPacks;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Extensions;
using modLib.Validators.Game;
using modLib.Validators.Mod;
using modLib.Validators.ModPack;
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

            builder.Services.AddValidators();

            builder.Services.AddScoped<ModService>();
            builder.Services.AddScoped<ModPackService>();
            builder.Services.AddScoped<GameService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}