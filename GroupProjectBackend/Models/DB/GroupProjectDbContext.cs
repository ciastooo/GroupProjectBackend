namespace GroupProjectBackend.Models.DB
{
    using GroupProjectBackend.Models.Dto;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GroupProjectDbContext : IdentityDbContext<ApplicationUser>
    {
        public GroupProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public GroupProjectDbContext()
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<CategoryModelDto> Categories { get; set; }
        public DbSet<PlaceModelDto> Places { get; set; }
        public DbSet<RatingModelDto> Ratings { get; set; }
        public DbSet<RouteModelDto> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.PhoneNumber);
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.PhoneNumberConfirmed);
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.Email);
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.EmailConfirmed);
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.NormalizedEmail);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
