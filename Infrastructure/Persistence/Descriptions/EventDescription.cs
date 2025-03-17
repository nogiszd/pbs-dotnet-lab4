using Microsoft.EntityFrameworkCore;

using WinLab4.Models;

namespace WinLab4.Infrastructure.Persistence.Descriptions;

internal static class EventDescription
{
    
    internal static void DescribeEvent(this ModelBuilder builder)
    {
        builder.Entity<Event>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            e.Property(x => x.Agenda)
                .IsRequired()
                .HasMaxLength(500);

            e.Property(x => x.Date)
                .IsRequired();

            e.HasMany(x => x.Reservations)
                .WithOne(x => x.Event)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
