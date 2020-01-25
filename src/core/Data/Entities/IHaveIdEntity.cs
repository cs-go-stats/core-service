using System;

namespace CSGOStats.Infrastructure.Core.Data.Entities
{
    public interface IHaveIdEntity : IEntity
    {
        Guid Id { get; }
    }
}