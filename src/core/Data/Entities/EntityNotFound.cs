using System;
using System.Runtime.Serialization;

namespace CSGOStats.Infrastructure.Core.Data.Entities
{
    [Serializable]
    public class EntityNotFound : Exception
    {
        public string Type { get; }

        public object Id { get; }

        private EntityNotFound(string type, object id)
        {
            Type = type;
            Id = id;
        }

        protected EntityNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static EntityNotFound For<T>(object id) => new EntityNotFound(
            type: typeof(T).FullName,
            id: id);
    }
}
