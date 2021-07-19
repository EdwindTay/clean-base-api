using System;
using Clean.DataAccess.EntityFramework;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Clean.Seed
{
    internal class SeedMain
    {
        private readonly IConfiguration _configuration;
        private readonly CleanDbContext _dbContext;

        public SeedMain(CleanDbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public void Run()
        {
            Log.Information("Starting Seeding Program");

            Log.Information($"Target Database: {_configuration.GetConnectionString("CleanDb")}");

            Log.Information("Press any key to continue...");
            Console.ReadKey();
            Log.Information("");

            bool shouldSeedUser = true;

            if (shouldSeedUser)
            RunSeedWrapper(Seeding.SeedUser);
        }

        private void RunSeedWrapper(Action<CleanDbContext> action)
        {
            string methodName = action.Method.Name;

            Log.Information($"Running Seeding: {methodName}");

            action(_dbContext);

            Log.Information($"Completed Seeding: {methodName}");
        }
    }
}
