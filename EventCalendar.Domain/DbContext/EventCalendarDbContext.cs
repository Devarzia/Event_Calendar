using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventCalendar.Domain
{
    public class EventCalendarDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, 
        ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public EventCalendarDbContext(DbContextOptions<EventCalendarDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseLazyLoadingProxies();

        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SocialEvent> SocialEvents { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new SocialEventConfiguration());
            modelBuilder.ApplyConfiguration(new ContactInfoConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
