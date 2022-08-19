using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCalendar.Domain
{
    internal class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            // Mapping To Table
            builder.ToTable("Log");

            // Mapping to Primary Key
            builder.HasKey(x => x.LogID);

            // Mapping Properties
            builder.Property(x => x.LogID).HasColumnName("LogID").IsRequired().UseIdentityColumn();
            builder.Property(x => x.LogDescription).HasColumnName("LogDescription").IsRequired();
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated").IsRequired();
            builder.Property(x => x.LogType).HasColumnName("LogType").IsRequired();
        }
    }
}
