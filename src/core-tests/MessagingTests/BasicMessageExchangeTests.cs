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
using NodaTime;
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
        public async Task PublishSubscribeTest()
        {
            Fixture.StartupBuilder.WithMessaging<CoreTestFixture>();

            var message = MessageBuilder.GenerateRandomMessage();

            await Fixture.RunAsync(
                new ActionsAggregator(
                    new InitializeMessagingAction(x => x.RegisterHandler<DelayedMessage>()),
                    new FunctorAction((services, _) => PublishSubscribeTestAsync(services, message))));

            await Task.Delay(TimeSpan.FromSeconds(.5));

            SharedData.Message.Id.Should().Be(message.Id);
            SharedData.Message.Version.Should().Be(message.Version);
            SharedData.Message.Data.Date.Should().Be(message.Data.Date);
            SharedData.Message.Data.Time.Should().Be(message.Data.Time);
        }

        private static Task PublishSubscribeTestAsync(IServiceProvider serviceProvider, TestMessage message) =>
            serviceProvider.GetService<IEventBus>().PublishAsync(message);

        [Fact]
        public Task AutopurgeQueueTest()
        {
            Fixture.StartupBuilder.WithMessaging<CoreTestFixture>();

            return Fixture.RunAsync(
                new ActionsAggregator(
                    new InitializeMessagingAction(x => x.RegisterHandler<TestMessage>()),
                    new FunctorAction(AutopurgeQueueTestAsync)));
        }

        private static Task AutopurgeQueueTestAsync(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var bus = serviceProvider.GetService<IEventBus>();
            return Task.WhenAll(Enumerable.Repeat(0, 10).Select(_ => bus.PublishAsync(MessageBuilder.GenerateRandomMessage())));
        }

        [Fact]
        public async Task DelayMessageTest()
        {
            Fixture.StartupBuilder.WithMessaging<CoreTestFixture>();

            var message = MessageBuilder.GenerateForDelay();

            await Fixture.RunAsync(
                new ActionsAggregator(
                    new InitializeMessagingAction(x => x.RegisterHandler<DelayedMessage>()),
                    new FunctorAction((serviceProvider, _) => DelayMessageTestAsync(serviceProvider, message))));

            await Task.Delay(TimeSpan.FromSeconds(.5));

            SharedData.DelayedMessage.Id.Should().Be(message.Id);
        }

        private static Task DelayMessageTestAsync(IServiceProvider serviceProvider, DelayedMessage message) =>
            serviceProvider.GetService<IEventBus>().ScheduleAsync(Duration.FromSeconds(.25D), message);
    }
}