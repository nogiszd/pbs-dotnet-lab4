using Microsoft.EntityFrameworkCore;

using WinLab4.Models.Enums;
using WinLab4.Models;

namespace WinLab4.Infrastructure.Persistence.Descriptions;

internal static class UserDescription
{
    internal static void DescribeUser(this ModelBuilder builder)
    {
        builder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            e.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            e.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(30);

            e.Property(x => x.Email)
                .IsRequired();

            e.Property(x => x.PasswordHash)
                .IsRequired();

            e.Property(x => x.Role)
                .HasDefaultValue(UserRole.User)
                .IsRequired();

            e.Property(x => x.FailedLoginAttempts)
                .HasDefaultValue(0)
                .IsRequired();

            e.HasIndex(x => x.Username)
                .IsUnique();

            e.HasIndex(x => x.Email)
                .IsUnique();
        });
    }
}
