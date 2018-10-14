using System;
namespace GroupProjectBackend.Models.DB
{
    using Microsoft.EntityFrameworkCore;
    using Config;

    public partial class GroupProjectContext : DbContext
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GroupProjectContext(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public GroupProjectContext(DbContextOptions<GroupProjectContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configurationProvider.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
