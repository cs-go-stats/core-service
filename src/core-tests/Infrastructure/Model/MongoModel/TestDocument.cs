using System;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Extensions;
using CSGOStats.Infrastructure.Core.Validation;
using NodaTime;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.MongoModel
{
    public class TestDocument : TestDocumentBase, ICloneable
    {
        public InnerDocument Inner { get; private set; }

        public long Version { get; private set; }

        public OffsetDateTime UpdatedOn { get; private set; }

        public TestDocument(Guid id, InnerDocument inner, long version, OffsetDateTime updatedOn)
            : base(id)
        {
            Inner = inner.NotNull(nameof(inner));
            Version = version;
            UpdatedOn = updatedOn;
        }

        public void Update()
        {
            Inner.Update();
            Version++;
            UpdatedOn = DatetimeUtils.GetCurrentDate;
        }

        public object Clone() => new TestDocument(
            id: Id,
            inner: Inner.Clone().OfType<InnerDocument>(),
            version: Version,
            updatedOn: UpdatedOn);

        public static TestDocument CreateRandomDocument()
        {
            var random = new Random();
            return new TestDocument(
                id: Guid.NewGuid(),
                inner: new InnerDocument(
                    data: Guid.NewGuid().ToString("N"),
                    count: random.Next(),
                    lockDate: null),
                version: random.Next(0, int.MaxValue),
                updatedOn: DatetimeUtils.GetCurrentDate);
        }
    }
}