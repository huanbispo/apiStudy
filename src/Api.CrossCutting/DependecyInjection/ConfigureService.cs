using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependecyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependecyInjectionServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
