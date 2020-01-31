using System;
using System.Linq;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Communication.Transport;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.Tests.MessagingTests.Messages;
using CSGOStats.Infrastructure.Core.Tests.MessagingTests.State;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.MessagingTests
{
    public class BasicMessageExchangeTests : FixtureBasedTest
    {
        public BasicMessageExchangeTests(CoreTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task PublishSubscribeTestAsync()
        {
            Fixture.StartupBuilder.WithMessaging<CoreTestFixture>();

            var message = MessageBuilder.GenerateRandomMessage();

            await Fixture.RunAsync(
                new ActionsAggregator(
                    new RegisterMessageHandlerForTypesAction(typeof(TestMessage)),
                    new FunctorAction((services, _) => PublishSubscribeTest(services, message))));

            await Task.Delay(TimeSpan.FromSeconds(.5));

            SharedData.Message.Id.Should().Be(message.Id);
            SharedData.Message.Version.Should().Be(message.Version);
            SharedData.Message.Data.Date.Should().Be(message.Data.Date);
            SharedData.Message.Data.Time.Should().Be(message.Data.Time);
        }

        [Fact]
        public Task AutopurgeQueueTestAsync()
        {
            Fixture.StartupBuilder.WithMessaging<CoreTestFixture>();

            return Fixture.RunAsync(
                new ActionsAggregator(
                    new RegisterMessageHandlerForTypesAction(typeof(TestMessage)),
                    new FunctorAction(AutopurgeQueueTest)));
        }

        private static Task PublishSubscribeTest(IServiceProvider serviceProvider, TestMessage message) =>
            serviceProvider.GetService<IEventBus>().PublishAsync(message);

        private static Task AutopurgeQueueTest(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var bus = serviceProvider.GetService<IEventBus>();
            return Task.WhenAll(Enumerable.Repeat(0, 10).Select(_ => bus.PublishAsync(MessageBuilder.GenerateRandomMessage())));
        }
    }
}