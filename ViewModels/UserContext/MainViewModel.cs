using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.Views.CommonContext;
using WinLab4.Views.UserContext;

namespace WinLab4.ViewModels.UserContext;

public class MainViewModel : BaseViewModel
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly AuthenticationService _authService;

    public ObservableCollection<Reservation> Reservations { get; } = new();

    public ICommand SignOutCommand { get; }

    public MainViewModel(IRepository<Reservation> reservationRepository, AuthenticationService authenticationService)
    {
        _reservationRepository = reservationRepository;
        _authService = authenticationService;

        SignOutCommand = new RelayCommand(_ => SignOut());

        _ = LoadReservations();
    }

    public async Task LoadReservations()
    {
        var user = _authService.CurrentUser;

        if (user == null) return;

        var reservations = await _reservationRepository.Find(
            x => x.User != null && x.User.Id == user.Id, 
            x => x.Include(r => r.User).Include(r => r.Event)
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

    private void SignOut()
    {
        _authService.Logout();
        var loginWindow = App.GetService<LoginWindow>();
        loginWindow.Show();
        Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
    }
}
