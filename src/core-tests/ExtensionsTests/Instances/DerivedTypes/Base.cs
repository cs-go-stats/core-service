using System;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests.Instances.DerivedTypes
{
    public class Base
    {
        public int Data { get; } = new Random().Next();
    }
}