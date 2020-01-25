using System;
using CSGOStats.Infrastructure.Core.Data.Storage.Contexts.EF;
using CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Infrastructure.Core.Tests.Infrastructure.EF.Contexts
{
    public class TestEfContext : BaseDataContext
    {
        public TestEfContext()
            : base(new PostgreConnectionSettings(
                host: "127.0.0.1",
                database: $"Test-{DateTime.UtcNow.Ticks}",
                username: "postgres",
                password: "dotFive1",
                isAuditEnabled: false))
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
        }
    }
}