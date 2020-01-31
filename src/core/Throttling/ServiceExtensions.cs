using CSGOStats.Infrastructure.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Throttling
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddThrottlingFromConfiguration(
            this IServiceCollection services, 
            IConfigurationRoot configuration,
            string sectionName) => services.AddScoped<IThrottlingAgent, StaticThrottlingAgent>(s =>
            {
                return new StaticThrottlingAgent(
                    configuration.GetFromConfiguration(
                        sectionName,
                        c => new ThrottlingSetting(c["Delay"].Timespan())));
            });
    }
}