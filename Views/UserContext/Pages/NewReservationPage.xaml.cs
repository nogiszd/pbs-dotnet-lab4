using System.Windows;
using System.Windows.Controls;

using WinLab4.ViewModels.UserContext.Pages;

namespace WinLab4.Views.UserContext.Pages;

/// <summary>
/// Logika interakcji dla klasy NewReservationPage.xaml
/// </summary>
public partial class NewReservationPage : Page
{
    private readonly NewReservationViewModel _viewModel;

    public NewReservationPage(NewReservationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;

        IsVisibleChanged += NewReservationPage_IsVisibleChanged;
    }

    private async void NewReservationPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            await _viewModel.LoadAvailableEvents();
        }
    }
}
