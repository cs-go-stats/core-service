using System;

namespace CSGOStats.Infrastructure.Core.Communication.Handling
{
    public interface IMessageRegistrar
    {
        void RegisterForType(Type type);
    }
}