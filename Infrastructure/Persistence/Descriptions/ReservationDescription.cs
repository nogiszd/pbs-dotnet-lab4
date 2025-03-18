using Microsoft.EntityFrameworkCore;

using WinLab4.Models;
using WinLab4.Models.Enums;

namespace WinLab4.Infrastructure.Persistence.Descriptions;

internal static class ReservationDescription
{
    internal static void DescribeReservation(this ModelBuilder builder)
    {
        builder.Entity<Reservation>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.ParticipationType).IsRequired();

            e.Property(x => x.CateringPreference).IsRequired();

            e.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Event)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
