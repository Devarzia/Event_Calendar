using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventCalendar.Domain
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfo");

            builder.HasKey(x => x.ContactInfoID);

            builder.Property(x => x.ContactInfoID).HasColumnName("ContactInforID").IsRequired().UseIdentityColumn();
            builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnName("Email").IsRequired();
            builder.Property(x => x.Message).HasColumnName("Message").IsRequired();
        }
    }
}
