using System;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Services.Core.Handling.Entities
{
    public abstract class AggregateRoot : IHaveIdEntity
    {
        public Guid Id { get; private set; }

        public long Version { get; private set; }

        protected AggregateRoot() { }

        protected AggregateRoot(Guid id, long version)
        {
            Id = id.AnythingBut(Guid.Empty, nameof(id));
            Version = version.Positive(nameof(version));
        }

        protected void Update() => Version++;
    }
}