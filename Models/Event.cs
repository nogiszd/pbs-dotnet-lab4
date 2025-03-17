namespace WinLab4.Models;

public sealed class Event : Entity
{
    public string Name { get; private set; } = null!;

    public string Agenda { get; private set; } = null!;

    public DateTime Date { get; private set; }

    public List<Reservation> Reservations { get; private set; } = new();

    public Event(string name, string agenda, DateTime date)
    {
        Name = name;
        Agenda = agenda;
        Date = date;
    }

    public void Update(string name, string agenda, DateTime date)
    {
        Name = name;
        Agenda = agenda;
        Date = date;
    }
}
