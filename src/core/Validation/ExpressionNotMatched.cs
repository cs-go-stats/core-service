using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.Core.Validation
{
    [Serializable]
    public class ExpressionNotMatched : Exception
    {
        public ExpressionNotMatched()
        {
        }

        protected ExpressionNotMatched(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
