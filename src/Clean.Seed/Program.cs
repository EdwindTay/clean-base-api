using System.IO;
using Clean.DataAccess.Core.Context;
using Clean.DataAccess.DependencyInjection;
using Clean.Seed.Core.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Clean.Seed
{
    class Program
    {
        static void Main()
        {
            //read appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            //setup serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            //setup DI
            var services = new ServiceCollection();

            //setup data access
            services.ConfigureDataAccess(configuration.GetConnectionString("CleanDb"));

            services.AddSingleton<IConfiguration>(provider => configuration);
            services.AddTransient<IUserContext, UserInformation>();
            services.AddTransient<SeedMain>();

            var serviceProvider = services.BuildServiceProvider();

            //run
            serviceProvider.GetService<SeedMain>().Run();
        }
    }
}
