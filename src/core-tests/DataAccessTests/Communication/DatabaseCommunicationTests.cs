using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Data.Entities;
using CSGOStats.Infrastructure.Core.Data.Storage.Repositories;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Initialization.RT.Actions;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Communication
{
    public class DatabaseCommunicationTests : FixtureBasedTest
    {
        public DatabaseCommunicationTests(CoreTestFixture fixture) 
            : base(fixture)
        {
        }

        [Fact]
        public Task RelationalAtomicCrudTests() => Fixture.RunAsync(new FunctorAction(RelationalAtomicCrudTest));

        [Fact]
        public Task DocumentOrientedAtomicCrudTests() => Fixture.RunAsync(new FunctorAction(DocumentOrientedAtomicCrudTest));

        private static Task RelationalAtomicCrudTest(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var entity = CreateTestEntity();
            var (entityId, entityDate) = (entity.Id, entity.Date);

            return AtomicCrudTestsTemplate(
                entityId,
                entity,
                serviceProvider.GetService<IRepository<TestEntity>>(),
                x => x.Update(),
                x => x.Date.Should().Be(entityDate),
                (x, y) => x.Date.Should().Be(y.Date).And.NotBe(entityDate));
        }

        private static Task DocumentOrientedAtomicCrudTest(IServiceProvider serviceProvider, IConfigurationRoot _)
        {
            var document = CreateTestDocument();
            var clone = document.Clone().OfType<TestDocument>();

            return AtomicCrudTestsTemplate(
                document.Id,
                document,
                serviceProvider.GetService<IRepository<TestDocument>>(),
                x => x.Update(),
                x =>
                {
                    x.Inner.Data.Should().Be(clone.Inner.Data);
                    x.Inner.Count.Should().Be(clone.Inner.Count);
                    x.Version.Should().Be(clone.Version);
                    x.UpdatedOn.Should().Be(clone.UpdatedOn);
                },
                (x, y) =>
                {
                    x.Inner.Data.Should().Be(x.Inner.Data).And.NotBe(clone.Inner.Data);
                    x.Inner.Count.Should().Be(x.Inner.Count).And.NotBe(clone.Inner.Count);
                    x.Version.Should().Be(x.Version).And.NotBe(clone.Version);
                    x.UpdatedOn.Should().Be(x.UpdatedOn).And.NotBe(clone.UpdatedOn);
                });
        }

        private static TestEntity CreateTestEntity() => TestEntity.CreateEmpty();

        private static TestDocument CreateTestDocument() => TestDocument.CreateRandomDocument();

        private static async Task AtomicCrudTestsTemplate<TKey, TState>(
            TKey key,
            TState initialState,
            IRepository<TState> repository,
            Action<TState> updateEntityFunctor,
            Action<TState> storeVerificationFunctor,
            Action<TState, TState> updateVerificationFunctor)
            where TState : class, IEntity
        {
            await repository.AddAsync(key, initialState);

            var storedState = await repository.GetAsync(key);
            storeVerificationFunctor(storedState);

            updateEntityFunctor(storedState);
            await repository.UpdateAsync(key, storedState);

            var updatedState = await repository.GetAsync(key);
            updateVerificationFunctor(updatedState, storedState);

            await repository.DeleteAsync(key, updatedState);
            await repository.FindAsync(key).ContinueWith(x => x.Result.Should().BeNull());

            var exception = await Record.ExceptionAsync(() => repository.GetAsync(key))
                .ContinueWith(x => x.Result.Should().BeOfType<EntityNotFound>().Subject);
            exception.Type.Should().Be(typeof(TState).FullName);
            exception.Id.Should().BeOfType<Guid>().And.Be(key);
        }
    }
}