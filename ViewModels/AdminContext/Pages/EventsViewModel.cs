using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.Views.AdminContext;

namespace WinLab4.ViewModels.AdminContext.Pages;

public class EventsViewModel : BaseViewModel
{
    private readonly IRepository<Event> _eventRepository;
    private Event? _selectedEvent;

    public ObservableCollection<Event> Events { get; } = new();

    public Event? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged(nameof(SelectedEvent));
        }
    }

    public bool IsEventSelected => SelectedEvent != null;

    public ICommand AddEventCommand { get; }
    public ICommand EditEventCommand { get; }
    public ICommand DeleteEventCommand { get; }

    public EventsViewModel(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;

        AddEventCommand = new RelayCommand(_ => AddEvent());
        EditEventCommand = new RelayCommand(_ => EditEvent());
        DeleteEventCommand = new AsyncRelayCommand(async (param) => await DeleteEvent(), _ => IsEventSelected);

        _ = LoadEvents();
    }

    public async Task LoadEvents()
    {
        var events = await _eventRepository.Find(_ => true);

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Events.Clear();
            foreach (var @event in events)
            {
                Events.Add(@event);
            }
        });
    }

    private async void AddEvent()
    {
        var addEventWindow = App.GetService<AddEventWindow>();
        addEventWindow.ShowDialog();
        await LoadEvents();
    }

    private async void EditEvent()
    {
        if (SelectedEvent == null) return;

        var viewModel = new EditEventViewModel(_eventRepository, SelectedEvent);

        var editEventWindow = App.GetService<EditEventWindow>();
        editEventWindow.DataContext = viewModel;

        editEventWindow.ShowDialog();

        await LoadEvents();
    }

    private async Task DeleteEvent()
    {
        if (SelectedEvent == null) return;

        var selectedEvent = await _eventRepository.Get(SelectedEvent.Id, x => x.Include(e => e.Reservations));

        if (selectedEvent.Reservations.Count != 0)
        {
            MessageBox.Show(
                "Nie można usunąć wydarzenia, które ma rezerwacje", 
                "Błąd", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error
                );
            return;
        }

        var result = MessageBox.Show(
            $"Czy na pewno chcesz usunąć wydarzenie {selectedEvent.Name}?",
            "Usuwanie wydarzenia",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
            );

        if (result == MessageBoxResult.Yes)
        {
            await _eventRepository.Delete(selectedEvent);
            await LoadEvents();
        }
    }
}
