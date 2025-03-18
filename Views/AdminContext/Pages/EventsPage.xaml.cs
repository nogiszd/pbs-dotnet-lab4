using System.Windows;
using System.Windows.Controls;

using WinLab4.ViewModels.AdminContext.Pages;

namespace WinLab4.Views.AdminContext.Pages;

/// <summary>
/// Logika interakcji dla klasy EventsPage.xaml
/// </summary>
public partial class EventsPage : Page
{
    private readonly EventsViewModel _viewModel;

    public EventsPage(EventsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;

        IsVisibleChanged += EventsPage_IsVisibleChanged;
    }

    private async void EventsPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            await _viewModel.LoadEvents();
        }
    }
}
