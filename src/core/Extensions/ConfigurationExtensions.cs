using System;
using CSGOStats.Infrastructure.Core.Validation;
using Microsoft.Extensions.Configuration;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSetting GetFromConfiguration<TSetting>(
            this IConfiguration configuration,
            string sectionName,
            Func<IConfiguration, TSetting> creatingFunctor)
            where TSetting : class
        {
            var section = configuration.NotNull(nameof(configuration)).GetSection(sectionName.NotNull(nameof(sectionName)));
            return creatingFunctor.NotNull(nameof(creatingFunctor)).Invoke(section);
        }
    }
}