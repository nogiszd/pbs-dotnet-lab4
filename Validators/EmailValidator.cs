using System.Text.RegularExpressions;

namespace WinLab4.Validators;

public static class EmailValidator
{
    // regex for email
    private static readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public static bool IsValid(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, emailPattern);
    }
}
