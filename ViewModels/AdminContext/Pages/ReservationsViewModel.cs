using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;

namespace WinLab4.ViewModels.AdminContext.Pages;

public class ReservationsViewModel : BaseViewModel
{
    private readonly IRepository<Reservation> _reservationRepository;
    private Reservation? _selectedReservation;

    public ObservableCollection<Reservation> Reservations { get; } = new();

    public Reservation? SelectedReservation
    {
        get => _selectedReservation;
        set
        {
            _selectedReservation = value;
            OnPropertyChanged(nameof(SelectedReservation));
        }
    }

    public bool IsReservationSelected => SelectedReservation != null;

    public bool CanAcceptReservation => IsReservationSelected && SelectedReservation!.IsAccepted;

    public bool CanRejectReservation => !CanAcceptReservation;

    public ICommand AcceptReservation { get; }
    public ICommand RejectReservation { get; }

    public ReservationsViewModel(IRepository<Reservation> reservationRepository)
    {
        _reservationRepository = reservationRepository;

        _ = LoadReservations();
    }

    public async Task LoadReservations()
    {
        var reservations = await _reservationRepository.Find(
            _ => true, 
            x => x.Include(s => s.User).Include(s => s.Event)
            );

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Reservations.Clear();
            foreach (var reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        });
    }
}
