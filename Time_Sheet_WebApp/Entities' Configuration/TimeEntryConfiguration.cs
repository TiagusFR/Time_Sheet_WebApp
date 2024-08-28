using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Time_Sheet_WebApp.Entities;

namespace Time_Sheet_WebApp.EntitiesConfiguration
{
    public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.HasKey(t => t.Id); 
            builder.Property(t => t.UserId).IsRequired(); 
            builder.Property(t => t.CheckInTime).IsRequired();
            builder.Property(t => t.CheckOutTime).IsRequired();

            // Relations
            builder.HasOne(t => t.User)
                .WithMany(u => u.TimeEntries)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
