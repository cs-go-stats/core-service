﻿using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Handling;
using CSGOStats.Infrastructure.Core.Communication.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSGOStats.Infrastructure.Core.Initialization.RT.Actions
{
    public class InitializeMessagingAction : ActionsAggregator
    {
        public InitializeMessagingAction(params Type[] messageTypes)
            : base(
                new RegisterMessageHandlerForTypesAction(messageTypes),
                new StopMessagingBusAction(),
                new StartHandlingMessagesAction())
        {
        }
    }

    internal class StopMessagingBusAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration) => Task.CompletedTask;

        public Task StopAsync(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IEventBus>().StopAsync();
    }

    internal class StartHandlingMessagesAction : IRuntimeAction
    {
        public Task ActAsync(IServiceProvider serviceProvider, IConfigurationRoot _) => 
            serviceProvider.GetService<IEventBus>().StartAsync();

        public Task StopAsync(IServiceProvider serviceProvider) => Task.CompletedTask;
    }

    internal class RegisterMessageHandlerForTypesAction : IRuntimeAction
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