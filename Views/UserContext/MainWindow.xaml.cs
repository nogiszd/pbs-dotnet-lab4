using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels.UserContext;
using WinLab4.ViewModels.UserContext.Pages;
using WinLab4.Views.UserContext.Pages;

namespace WinLab4.Views.UserContext;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel, IRepository<Reservation> reservationRepository, IRepository<Event> eventRepository, AuthenticationService authService)
    {
        InitializeComponent();
        DataContext = viewModel;

        NewReservationFrame.Content = new NewReservationPage(
            new NewReservationViewModel(
                reservationRepository, 
                eventRepository, 
                authService, 
                async () => await viewModel.LoadReservations()
                )
            );
    }
}