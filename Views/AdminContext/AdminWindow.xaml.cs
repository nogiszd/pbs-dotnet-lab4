using System.Windows;

using WinLab4.ViewModels.AdminContext;
using WinLab4.ViewModels.AdminContext.Pages;
using WinLab4.Views.AdminContext.Pages;

namespace WinLab4.Views.AdminContext;

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
