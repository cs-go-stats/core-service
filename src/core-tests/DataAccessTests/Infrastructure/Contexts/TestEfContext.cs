using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Infrastructure.Core.Tests.DataAccessTests.Infrastructure.Contexts
{
    public class TestEfContext : BaseDataContext
    {
        public TestEfContext(PostgreConnectionSettings settings)
            : base(settings)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
        }
    }
}