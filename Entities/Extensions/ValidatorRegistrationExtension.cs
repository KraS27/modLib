using FluentValidation;
using modLib.Entities.DTO.Auth;
using modLib.Entities.DTO.Game;
using modLib.Entities.DTO.ModPacks;
using modLib.Entities.DTO.Mods;
using modLib.Validators.Auth;
using modLib.Validators.Game;
using modLib.Validators.Mod;
using modLib.Validators.ModPack;

namespace modLib.Entities.Extensions
{
    public static class ValidatorRegistrationExtension
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateModDTO>, CreateModDTOValidator>();
            services.AddTransient<IValidator<UpdateModDTO>, UpdateModDTOValidator>();

            services.AddTransient<IValidator<CreateModPackDTO>, CreateModPackDTOValidator>();
            services.AddTransient<IValidator<UpdateModPackDTO>, UpdateModPackDTOValidator>();

            services.AddTransient<IValidator<CreateGameDTO>, CreateGameDTOValidator>();
            services.AddTransient<IValidator<UpdateGameDTO>, UpdateGameDTOValidator>();

            services.AddTransient<IValidator<RegisterModel>, RegisterModelValidation>();
        }
    }
}
