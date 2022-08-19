using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCalendar.Domain
{
    internal class SocialEventConfiguration : IEntityTypeConfiguration<SocialEvent>
    {
        public void Configure(EntityTypeBuilder<SocialEvent> builder)
        {
            // Mapping Table
            builder.ToTable("SocialEvent");

            // Mapping Primary Key
            builder.HasKey(x => x.SocialEventID);

            //Mapping Foreign Key
            builder.HasMany<EventCategory>().WithOne();
            builder.HasMany<ApplicationUser>().WithOne();

            // Mapping Properties
            builder.Property(x => x.SocialEventID).HasColumnName("SocialEventID").IsRequired().UseIdentityColumn();
            builder.Property(x => x.SocialEventName).HasColumnName("SocialEventName").IsRequired().HasMaxLength(35);
            builder.Property(x => x.SocialEventDescription).HasColumnName("SocialEventDescription").IsRequired().HasMaxLength(75);
            builder.Property(x => x.StartTime).HasColumnName("StartDate").IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.EndTime).HasColumnName("EndTime").HasColumnType("datetime2");
            builder.Property(x => x.AllDay).HasColumnName("IsAllDayEvent").IsRequired();
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.DateModified).HasColumnName("DateModified").IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.CategoryID).HasColumnName("CategoryID").IsRequired();
            builder.Property(x => x.UserID).HasColumnName("UserID").IsRequired();
        }
    }
}
