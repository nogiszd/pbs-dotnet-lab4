using System.Windows;
using System.Windows.Controls;

using WinLab4.ViewModels;

namespace WinLab4.Views.Pages;

/// <summary>
/// Logika interakcji dla klasy ReservationsPage.xaml
/// </summary>
public partial class ReservationsPage : Page
{
    private readonly ReservationsViewModel _viewModel;

    public ReservationsPage(ReservationsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;

        IsVisibleChanged += ReservationsPage_IsVisibleChanged;
    }

    private async void ReservationsPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            await _viewModel.LoadReservations();
        }
    }
}
