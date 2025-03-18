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

    public AdminWindow(AdminViewModel adminViewModel, UsersViewModel usersViewModel, EventsViewModel eventsViewModel)
    {
        InitializeComponent();
        DataContext = adminViewModel;

        _usersViewModel = usersViewModel;
        _eventsViewModel = eventsViewModel;

        UsersFrame.Content = new UsersPage(_usersViewModel);
        EventsFrame.Content = new EventsPage(_eventsViewModel);
    }
}
