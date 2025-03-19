using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using WinLab4.Commands.UserContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.Models.Enums;

namespace WinLab4.ViewModels.UserContext.Pages;

public class NewReservationViewModel : BaseViewModel
{
    private readonly IRepository<Event> _eventRepository;
    private readonly AuthenticationService _authService;

    public ObservableCollection<Event> AvailableEvents { get; } = new();
    public ObservableCollection<CateringPreference> CateringPreferences { get; } = new();
    public ObservableCollection<ParticipationType> ParticipationTypes { get; } = new();

    private Event? _selectedEvent;
    private CateringPreference? _selectedCateringPreference;
    private ParticipationType? _selectedParticipationType;

    public Event? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanSelectAttributes));
        }
    }

    public CateringPreference? SelectedCateringPreference
    {
        get => _selectedCateringPreference;
        set
        {
            _selectedCateringPreference = value;
            OnPropertyChanged();
        }
    }

    public ParticipationType? SelectedParticipationType
    {
        get => _selectedParticipationType;
        set
        {
            _selectedParticipationType = value;
            OnPropertyChanged();
        }
    }

    public bool CanSelectAttributes => SelectedEvent != null;

    public ICommand NewReservationCommand { get; }

    public NewReservationViewModel(IRepository<Reservation> reservationRepository, IRepository<Event> eventRepository, AuthenticationService authService, Action refreshList)
    {
        _eventRepository = eventRepository;
        _authService = authService;

        NewReservationCommand = new NewReservationCommand(reservationRepository, authService, this, refreshList);

        CateringPreferences = new ObservableCollection<CateringPreference>(Enum.GetValues<CateringPreference>());
        ParticipationTypes = new ObservableCollection<ParticipationType>(Enum.GetValues<ParticipationType>());

        _ = LoadAvailableEvents();
    }

    public async Task LoadAvailableEvents()
    {
        var user = _authService.CurrentUser;

        if (user == null) return;

        var events = await _eventRepository.Find(
            x => !x.Reservations.Any(y => y.UserId == user.Id),
            x => x.Include(e => e.Reservations)
            );

        await Application.Current.Dispatcher.InvokeAsync(() => 
        {
            AvailableEvents.Clear();
            foreach (var @event in events)
            {
                AvailableEvents.Add(@event);
            }
        });
    }
}
