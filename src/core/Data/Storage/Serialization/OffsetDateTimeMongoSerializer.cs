using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Extensions;
using CSGOStats.Infrastructure.Core.Extensions;
using MongoDB.Bson.Serialization;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Serialization
{
    public class OffsetDateTimeMongoSerializer : IBsonSerializer
    {
        public static IBsonSerializer Instance { get; } = new OffsetDateTimeMongoSerializer();

        public Type ValueType => typeof(OffsetDateTime);

        private OffsetDateTimeMongoSerializer()
        {
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            context.Reader.ReadString().Deserialize();

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value) =>
            context.Writer.WriteString(value.OfType<OffsetDateTime>().Serialize());
    }
}