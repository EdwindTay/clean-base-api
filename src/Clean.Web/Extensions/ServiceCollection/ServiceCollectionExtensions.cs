using Clean.Application.Configurations;
using Clean.Web.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Web.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Prepare ExceptionHandlingConfiguration and add it as a singleton for DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ExceptionHandlingConfiguration AddExceptionHandlingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ExceptionHandlingConfiguration exceptionHandlingConfiguration = new();
            configuration.GetSection("ExceptionHandling").Bind(exceptionHandlingConfiguration);
            services.AddSingleton(exceptionHandlingConfiguration);

            return exceptionHandlingConfiguration;
        }

        /// <summary>
        /// Prepare ConnectionStringsConfiguration and add it as a singleton for DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        public static ConnectionStringsConfiguration AddConnectionStringsConfiguration(this IServiceCollection services, IConfiguration configuration, string cryptoKey)
        {
            ConnectionStringsConfiguration connectionStringsConfiguration = new();
            configuration.GetSection("ConnectionStrings").Bind(connectionStringsConfiguration);
            connectionStringsConfiguration.Decrypt(cryptoKey);
            services.AddSingleton(connectionStringsConfiguration);

            return connectionStringsConfiguration;
        }

        /// <summary>
        /// Prepare FirebaseConfiguration and add it as a singleton for DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        public static FirebaseConfiguration AddFirebaseConfiguration(this IServiceCollection services, IConfiguration configuration, string cryptoKey)
        {
            FirebaseConfiguration firebaseConfiguration = new();
            configuration.GetSection("Firebase").Bind(firebaseConfiguration);
            firebaseConfiguration.Decrypt(cryptoKey);
            services.AddSingleton(firebaseConfiguration);

            return firebaseConfiguration;
        }

        /// <summary>
        /// Add Firebase Token Authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="firebaseConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddFirebaseTokenAuthentication(this IServiceCollection services, FirebaseConfiguration firebaseConfiguration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{firebaseConfiguration.ProjectId}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{firebaseConfiguration.ProjectId}",
                        ValidateAudience = true,
                        ValidAudience = $"{firebaseConfiguration.ProjectId}",
                        ValidateLifetime = true
                    };
                });

            return services;
        }
    }
}
