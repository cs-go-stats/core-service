using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class RegisterMessageHandlerForTypesAction : IRuntimeAction
    {
        private readonly Type[] _types;

        public RegisterMessageHandlerForTypesAction(params Type[] types)
        {
            _types = types;
        }

        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var registrar = serviceProvider.GetService<IMessageRegistrar>();

            foreach (var type in _types)
            {
                registrar.RegisterForType(type);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(IServiceProvider _) => Task.CompletedTask;
    }
}