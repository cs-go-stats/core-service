using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    [Serializable]
    public class UnwantedState : Exception
    {
        public string InstanceName { get; }

        public object Value { get; }

        public UnwantedState(string instanceName, object value)
            : base($"'{instanceName}' have value '{value}' that is invalid in current context.")
        {
            InstanceName = instanceName;
            Value = value;
        }

        protected UnwantedState(SerializationInfo info,  StreamingContext context) 
            : base(info, context)
        {
        }
    }
}