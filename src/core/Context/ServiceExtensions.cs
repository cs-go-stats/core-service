using System;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Context
{
    public static class ServiceExtensions
    {
        public static ExecutionContext InitializeInScope(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ExecutionContext>();
            context.InitializeFrom(ExecutionContext.CreateDefault());

            return context;
        }
    }
}