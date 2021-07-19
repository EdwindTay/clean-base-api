using System;
using System.Collections.Generic;
using System.Globalization;
using Clean.Application.DependencyInjection;
using Clean.Application.HttpClients;
using Clean.Application.HttpClients.Interfaces;
using Clean.DataAccess.Core.Context;
using Clean.DataAccess.DependencyInjection;
using Clean.Web.Core.Context;
using Clean.Web.Core.Serilog.Log;
using Clean.Web.Core.Swagger;
using Clean.Web.Extensions.ServiceCollection;
using Clean.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog;

namespace Clean.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CLEAN", Version = "v1" });

                //https://stackoverflow.com/questions/61540706/configure-swagger-authentication-with-firebase-google-in-net-core
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/api/auth/token", UriKind.Relative),
                            Extensions = new Dictionary<string, IOpenApiExtension>
                            {
                                { "returnSecureToken", new OpenApiBoolean(true) },
                            },
                        }

                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.OperationFilter<SwaggerLanguageHeader>();
            });

            string cryptoKey = Configuration["CryptoKey"];

            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddLocalization();

            var supportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("ms") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ms");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Config
            // ExceptionHandlingConfiguration
            var exceptionHandlingConfiguration = services.AddExceptionHandlingConfiguration(Configuration);
            // FirebaseConfiguration
            var firebaseConfiguration = services.AddFirebaseConfiguration(Configuration, cryptoKey);
            // ConnectionStringsConfiguration
            var connectionStringsConfiguration = services.AddConnectionStringsConfiguration(Configuration, cryptoKey);

            //firebase token authentication
            services.AddFirebaseTokenAuthentication(firebaseConfiguration);

            // Make IHttpContextAccessor available for dependency injection
            // Used to get http request context user id
            // This user id is set in custom middleware (UserInformationMiddleware / app.UseUserInformation())
            // This user id is used to update audit columns (CreatedById, UpdateById) automatically
            services.AddHttpContextAccessor();

            services.ConfigureDataAccess(connectionStringsConfiguration.CleanDb);
            services.ConfigureServices();
            services.AddScoped<IUserContext, UserInformation>();

            // http clients
            // use polly as resilience framework
            // https://www.nuget.org/packages/Microsoft.Extensions.Http.Polly
            services.AddHttpClient<IFirebaseAuthHttpClient, FirebaseAuthHttpClient>()
                 .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, item => TimeSpan.FromSeconds(4)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLEAN v1"));
            }

            app.UseSerilogRequestLogging(options =>
            {
                // Attach additional properties
                options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
            });

            app.UseRequestLocalization();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials() // allow credentials
            );

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to handle custom exception
            // Returns error in JSON format
            app.UseCustomExceptionHandling();

            // Enable middleware to inject User Id to http request context if User Identity (JWT token) is available
            // Gets the Firebase User Id from User Identity and retrieve User Id from database
            app.UseUserInformation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
