using System;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.Data.Entities
{
    public abstract class AggregateRoot : IHaveIdEntity
    {
        public Guid Id { get; private set; }

        public long Version { get; private set; }

        protected AggregateRoot() { }

        protected AggregateRoot(Guid id, long version)
        {
            Id = id;
            Version = version.Positive(nameof(version));
        }

        protected void Update() => Version++;
    }
}