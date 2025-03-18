using System.Windows;

using WinLab4.Infrastructure.Services;
using WinLab4.ViewModels;

namespace WinLab4.Views;

/// <summary>
/// Logika interakcji dla klasy NewPasswordWindow.xaml
/// </summary>
public partial class NewPasswordWindow : Window
{
    private readonly AuthenticationService _authService;

    public NewPasswordWindow(NewPasswordViewModel viewModel, AuthenticationService authService)
    {
        InitializeComponent();
        _authService = authService;
        DataContext = viewModel;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _authService.Logout();
    }
}
