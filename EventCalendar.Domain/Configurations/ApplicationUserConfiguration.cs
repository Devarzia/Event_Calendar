using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCalendar.Domain
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnName("FirstName");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnName("LastName");
            builder.Property(x => x.ProfilePicture).HasColumnName("ProfilePicture");
        }
    }
}
