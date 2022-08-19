using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCalendar.Domain
{
    internal class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            // Mapping to Table
            builder.ToTable("EventCategory");

            // Mapping to Primary Key
            builder.HasKey(x => x.EventCategoryID);

            // Mapping Properties
            builder.Property(x => x.EventCategoryID).HasColumnName("EventCategoryID").IsRequired().UseIdentityColumn();
            builder.Property(x => x.EventCategoryName).HasColumnName("EventCategoryName").IsRequired().HasMaxLength(35);
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").IsRequired().HasColumnType("datetime2");
            builder.Property(x => x.DateModified).HasColumnName("DateModified").IsRequired();
        }
    }
}
