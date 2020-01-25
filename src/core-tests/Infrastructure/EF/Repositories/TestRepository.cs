using CSGOStats.Infrastructure.Core.Data.Storage.Repositories.EF;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Contexts;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.Model.EFModel;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Repositories
{
    public class TestRepository : EfRepository<TestEntity>
    {
        public TestRepository()
            : this(new TestEfContext())
        {
        }

        public TestRepository(TestEfContext context)
            : base(context)
        {
        }
    }
}