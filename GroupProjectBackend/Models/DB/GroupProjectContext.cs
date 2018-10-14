using System;
namespace GroupProjectBackend.Models.DB
{
    using Microsoft.EntityFrameworkCore;
    using Config;

    public partial class GroupProjectContext : DbContext
    {
        public GroupProjectContext()
        {
        }

        public GroupProjectContext(DbContextOptions<GroupProjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
