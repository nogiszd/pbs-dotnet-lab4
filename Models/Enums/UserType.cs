using System.ComponentModel;

namespace WinLab4.Models.Enums;

public enum UserRole
{
    [Description("Użytkownik")]
    User = 1,

    [Description("Administrator")]
    Admin = 2,
}
