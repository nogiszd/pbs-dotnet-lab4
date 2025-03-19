using System.Windows.Input;

using WinLab4.Commands.AdminContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;

namespace WinLab4.ViewModels.AdminContext;

public class EditEventViewModel : BaseViewModel
{
    private Guid _id;
    private string _name = string.Empty;
    private string _agenda = string.Empty;
    private DateTime? _date;

    private readonly Event _originalEvent;

    public Guid Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Agenda
    {
        get => _name;
        set
        {
            _agenda = value;
            OnPropertyChanged();
        }
    }

    public DateTime? Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public ICommand EditEventCommand { get; }

    public EditEventViewModel(IRepository<Event> eventRepository, Event selectedEvent)
    {
        _originalEvent = selectedEvent;

        _id = _originalEvent.Id;
        _name = _originalEvent.Name;
        _agenda = _originalEvent.Agenda;
        _date = _originalEvent.Date;

        EditEventCommand = new EditEventCommand(eventRepository, this);
    }
}
