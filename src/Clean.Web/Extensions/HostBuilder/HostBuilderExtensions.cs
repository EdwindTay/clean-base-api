using System;
using Microsoft.Extensions.Hosting;

namespace Clean.Web.Extensions.HostBuilder
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder LoadAWSCredentialsFromSecretManagerForDevelopmentInDocker(
                    this IHostBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices((context, collections) =>
            {
                //load aws credential info from secret manager and put it into environment variables when running in local development in docker
                //this is to avoid having to put the aws credentials in appsettings or anywhere else in the source code that will be commited and pushed to repo
                //in any other environment, set the environment variable when configuring the docker container

                var _environment = context.HostingEnvironment;
                var isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.ToLower() == bool.TrueString.ToLower();

                if (_environment.IsDevelopment() && isRunningInContainer)
                {
                    string cryptoKey = context.Configuration["CryptoKey"];

                    string awsAccessKeyId = context.Configuration["AWS:AccessKeyId"];
                    string awsSecretAccessKey = context.Configuration["AWS:SecretAccessKey"];
                    string awsDefaultRegion = context.Configuration["AWS:DefaultRegion"];

                    Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", awsAccessKeyId);
                    Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", awsSecretAccessKey);
                    Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", awsDefaultRegion);
                }
            });

            return builder;
        }
    }
}
