using WinLab4.Models.Enums;

namespace WinLab4.Models;

public sealed class User : Entity
{
    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Username { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public bool NeedsNewPassword { get; private set; } = false;

    public string Email { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public UserRole Role { get; private set; } = UserRole.User;

    public int FailedLoginAttempts { get; private set; } = 0;

    public bool IsLockedOut => FailedLoginAttempts >= 3;

    public User(string firstName, string lastName, string username, string? passwordHash, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PasswordHash = passwordHash ?? string.Empty;
        Email = email;
    }

    public void EnforcePasswordChange()
    {
        NeedsNewPassword = true;
    }

    public void IncrementFailedLoginAttempts()
    {
        if (!IsLockedOut)
        {
            FailedLoginAttempts++;
        }
    }

    public void ResetFailedLoginAttempts()
    {
        FailedLoginAttempts = 0;
    }

    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        NeedsNewPassword = false;
    }
}