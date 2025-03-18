using System.Windows;
using WinLab4.ViewModels;
using WinLab4.Views.Pages;

namespace WinLab4.Views;

/// <summary>
/// Logika interakcji dla klasy AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    private readonly UsersViewModel _usersViewModel;
    private readonly EventsViewModel _eventsViewModel;
    private readonly ReservationsViewModel _reservationsViewModel;

    public AdminWindow(AdminViewModel adminViewModel, UsersViewModel usersViewModel, EventsViewModel eventsViewModel, ReservationsViewModel reservationsViewModel)
    {
        InitializeComponent();
        DataContext = adminViewModel;

        _usersViewModel = usersViewModel;
        _eventsViewModel = eventsViewModel;
        _reservationsViewModel = reservationsViewModel;

        UsersFrame.Content = new UsersPage(_usersViewModel);
        EventsFrame.Content = new EventsPage(_eventsViewModel);
        ReservationFrame.Content = new ReservationsPage(_reservationsViewModel);
    }
}
