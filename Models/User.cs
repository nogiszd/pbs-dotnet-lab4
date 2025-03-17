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

    public User(string firstName, string lastName, string username, string passwordHash, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
    }

    public void EnforcePasswordChange()
    {
        NeedsNewPassword = true;
    }
}