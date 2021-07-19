using Clean.DataAccess.Core.Context;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Users;
using Clean.DataAccess.Repositories.Users.Interfaces;
using Clean.DataAccess.UnitOfWork;
using Clean.DataAccess.UnitOfWork.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.DataAccess.DependencyInjection
{
    public static class Configure
    {
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, string cleanDbConnectionString)
        {
            //services.AddDbContext<GMSDbContext>(options =>
            //   options.UseInMemoryDatabase("database"));

            services.AddDbContext<CleanDbContext>(options =>
               options.UseSqlServer(
                   cleanDbConnectionString,
                   x => x.MigrationsAssembly("Clean.Migrations")
                )
            );

            //db context
            services.AddScoped<CleanDbContext>();
            services.AddScoped<IUnitOfWork<CleanDbContext>, CleanUnitOfWork>();
            services.AddScoped<IUserContext, UserContext>();

            //repositories
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
