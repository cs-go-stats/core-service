using System;

namespace CSGOStats.Infrastructure.Core.Context
{
    public class ExecutionContext
    {
        public Guid CorrelationId { get; private set; }

        public string ContextName { get; private set; }

        public string ContextParameter { get; private set; }

        public ExecutionContext()
        {
        }

        private ExecutionContext(Guid correlationId, string contextName, string contextParameter)
        {
            CorrelationId = correlationId;
            ContextName = contextName;
            ContextParameter = contextParameter;
        }

        internal void InitializeFrom(ExecutionContext context)
        {
            CorrelationId = context.CorrelationId;
            ContextName = context.ContextName;
            ContextParameter = context.ContextParameter;
        }

        internal void SetContext(string contextName, string contextParameter)
        {
            ContextName = contextName;
            ContextParameter = contextParameter;
        }

        public static ExecutionContext CreateDefault() => new ExecutionContext(
            correlationId: Guid.NewGuid(),
            contextName: null,
            contextParameter: null);
    }
}