using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Contexts
{
    public static class BsonMappings
    {
        public static void Register()
        {
            MongoMappingExtensions.TryRegisterClassMap<TestDocumentBase>(mapper => mapper.MapGuid(x => x.Id));

            MongoMappingExtensions.TryRegisterClassMap<TestDocument>(mapper =>
            {
                mapper.MapOptional(x => x.Inner);
                mapper.MapRequired(x => x.Version);
                mapper.MapOffsetDateTime(x => x.UpdatedOn);
                mapper.MapCreator(x => new TestDocument(x.Id, x.Inner, x.Version, x.UpdatedOn));
            });

            MongoMappingExtensions.TryRegisterClassMap<InnerDocument>(mapper =>
            {
                mapper.MapRequired(x => x.Data);
                mapper.MapRequired(x => x.Count);
                mapper.MapOffsetDateTime(x => x.LockDate);
            });
        }
    }
}