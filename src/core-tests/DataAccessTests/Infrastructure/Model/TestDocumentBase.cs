using System;
using CSGOStats.Infrastructure.Core.Data.Entities;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model
{
    public abstract class TestDocumentBase : IHaveIdEntity
    {
        public Guid Id { get; private set; }

        protected TestDocumentBase(Guid id)
        {
            Id = id;
        }
    }
}