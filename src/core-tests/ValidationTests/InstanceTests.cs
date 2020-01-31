using System;
using CSGOStats.Infrastructure.Core.Tests.ExtensionsTests.Instances.HaveInterfaceTypes;
using CSGOStats.Infrastructure.Core.Validation;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.Core.Tests.ValidationTests
{
    public class InstanceTests
    {
        private static readonly Random Random = new Random();

        [Fact]
        public void NotNullForClassesTest()
        {
            {
                var defaultClass = GetDefaultClassInstance();
                Record.Exception(() => defaultClass.NotNull(nameof(defaultClass))).Should().BeNull();
            }

            {
                var nullClass = GetNullClassInstance();
                const string parameterName = nameof(nullClass);
                var exception = Record.Exception(() => nullClass.NotNull(parameterName));
                exception.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be(parameterName);
            }
        }

        [Fact]
        public void NotNullForStructuresTest()
        {
            {
                var defaultStructure = GetDefaultStructInstance();
                Record.Exception(() => defaultStructure.NotNull(nameof(defaultStructure))).Should().BeNull();
            }

            {
                var nullStructure = GetNullStructInstance();
                const string parameterName = nameof(nullStructure);
                var exception = Record.Exception(() => nullStructure.NotNull(parameterName));
                exception.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be(parameterName);
            }
        }

        [Fact]
        public void UnwantedValuePositiveTest()
        {
            {
                var instance = Random.Next();
                var comparable = Random.Next();
                instance.AnythingBut(comparable, nameof(instance)).Should().Be(instance);
            }

            {
                var instance = GetDefaultClassStatefulInstance();
                var comparable = GetDefaultClassStatefulInstance();
                instance.AnythingBut(comparable, nameof(instance)).Data.Should().Be(instance.Data);
            }
        }

        [Fact]
        public void UnwantedValueNegativeTest()
        {
            {
                var instance = Random.Next();
                var comparable = instance;
                const string instanceName = nameof(instance);
                var exception = Record.Exception(() => instance.AnythingBut(comparable, instanceName)).Should().BeOfType<UnwantedState>().Subject;
                exception.InstanceName.Should().Be(instanceName);
                exception.Value.Should().BeOfType<int>().Which.Should().Be(instance);
            }

            {
                var instance = GetDefaultClassStatefulInstance();
                var comparable = new TestClass(instance.Data);
                const string instanceName = nameof(instance);
                var exception = Record.Exception(() => instance.AnythingBut(comparable, instanceName)).Should().BeOfType<UnwantedState>().Subject;
                exception.InstanceName.Should().Be(instanceName);
                exception.Value.Should().BeOfType<TestClass>().Which.Data.Should().Be(instance.Data);
            }
        }

        private static TestClass GetDefaultClassInstance() => new TestClass();

        private static TestClass GetDefaultClassStatefulInstance() => new TestClass(Random.Next());

        private static TestClass GetNullClassInstance() => null;

        private static TestStruct? GetDefaultStructInstance() => new TestStruct();

        private static TestStruct? GetNullStructInstance() => null;
    }
}