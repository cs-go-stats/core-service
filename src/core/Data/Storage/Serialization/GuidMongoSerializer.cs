using System;
using CSGOStats.Infrastructure.Core.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CSGOStats.Infrastructure.Core.Data.Storage.Serialization
{
    public class GuidMongoSerializer : GuidSerializer
    {
        internal const GuidRepresentation DefaultGuidRepresentation = GuidRepresentation.Standard;
        
        public static IBsonSerializer Instance { get; } = new GuidMongoSerializer();

        private GuidMongoSerializer()
            : base(DefaultGuidRepresentation)
        {
        }

        public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) =>
            context.Reader.ReadBinaryData().AsGuid;

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
        {
            var data = new BsonBinaryData(value.OfType<Guid>(), DefaultGuidRepresentation);
            context.Writer.WriteBinaryData(data);
        }
    }
}