using WinLab4.Models.Enums;

namespace WinLab4.Models;

public sealed class Reservation : Entity
{
    public Guid UserId { get; private set; }

    public Guid EventId { get; private set; }

    public ParticipationType ParticipationType { get; private set; }

    public CateringPreference CateringPreference { get; private set; }

    public bool IsAccepted { get; private set; } = false;

    public User? User { get; private set; }

    public Event? Event { get; private set; }

    public Reservation(Guid userId, Guid eventId, ParticipationType participationType, CateringPreference cateringPreference)
    {
        UserId = userId;
        EventId = eventId;
        ParticipationType = participationType;
        CateringPreference = cateringPreference;
    }

    public void ChangeAcceptation(bool flag)
    {
        IsAccepted = flag;
    }
}
