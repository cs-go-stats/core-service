using System;
using CSGOStats.Infrastructure.Core.Communication.Handling.Initialization;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.Communication.Handling
{
    internal class TestBusActivationHandler : BaseBusActivationHandler
    {
        public TestBusActivationHandler(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }
    }
}