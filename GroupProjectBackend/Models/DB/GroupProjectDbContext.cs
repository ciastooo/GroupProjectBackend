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

        protected GroupProjectDbContext()
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RoutePlace> RoutePlaces { get; set; }

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

            modelBuilder.Entity<Route>()
                .HasMany(e => e.RoutePlaces)
                .WithOne(e => e.Route)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
