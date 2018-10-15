namespace GroupProjectBackend.Models.DB
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GroupProjectDbContext : IdentityDbContext<ApplicationUser>
    {
        public GroupProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected GroupProjectDbContext()
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    }
}
