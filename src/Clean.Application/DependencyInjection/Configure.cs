using Clean.Application.Services.Users;
using Clean.Application.Services.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application.DependencyInjection
{
    public static class Configure
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
