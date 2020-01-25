using System;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.DerivedTypes
{
    public class Base
    {
        public int Data { get; } = new Random().Next();
    }
}