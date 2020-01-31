using System;

namespace CSGOStats.Infrastructure.Core.Tests.ExtensionsTests.Instances.HaveInterfaceTypes
{
    public class TestClass : ITestInterface, IEquatable<TestClass>
    {
        public int Data { get; }

        public TestClass()
        {
        }

        public TestClass(int data)
        {
            Data = data;
        }

        public bool Equals(TestClass other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Data == other.Data;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestClass) obj);
        }

        public override int GetHashCode() => Data;
    }
}