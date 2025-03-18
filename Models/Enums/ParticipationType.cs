using System.ComponentModel;

namespace WinLab4.Models.Enums;

public enum ParticipationType
{
    [Description("Autor")]
    Speaker = 1,

    [Description("Słuchacz")]
    Attendee = 2,

    [Description("Sponsor")]
    Sponsor = 3,

    [Description("Organizator")]
    Organizer = 4
}
