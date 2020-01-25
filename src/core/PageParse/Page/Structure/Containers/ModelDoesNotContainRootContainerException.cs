using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers
{
    [Serializable]
    public class ModelDoesNotContainRootContainerException : Exception
    {
        public ModelDoesNotContainRootContainerException(Type modelType)
            : base($"Model of type '{modelType.FullName}' cannot be mapped to a whole HTML page.")
        {
        }

        protected ModelDoesNotContainRootContainerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}