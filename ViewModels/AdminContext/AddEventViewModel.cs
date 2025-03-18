using System.Windows.Input;

using WinLab4.Commands.AdminContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;

namespace WinLab4.ViewModels.AdminContext;

public class AddEventViewModel : BaseViewModel
{
    private string _name = string.Empty;
    private string _agenda = string.Empty;
    private DateTime? _date;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Agenda
    {
        get => _agenda;
        set
        {
            _agenda = value;
            OnPropertyChanged(nameof(Agenda));
        }
    }

    public DateTime? Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    public ICommand AddEventCommand { get; }

    public AddEventViewModel(IRepository<Event> eventRepository)
    {
        AddEventCommand = new AddEventCommand(eventRepository, this);
    }
}
