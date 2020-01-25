using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace CSGOStats.Infrastructure.Core.Initialization
{
    public class Startup
    {
        public static StartupBuilder ForEnvironment(string serviceName, string environment) =>
            new StartupBuilder(
                serviceName: serviceName,
                configuration: CreateConfiguration(new HostingEnvironment { EnvironmentName = environment }));

        private static IConfigurationRoot CreateConfiguration(IHostEnvironment environment) => new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}