using System.Windows;
using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Services;
using WinLab4.Views;

namespace WinLab4.ViewModels;

public class AdminViewModel
{
    private readonly AuthenticationService _authService;

    public ICommand SignOutCommand { get; }

    public AdminViewModel(AuthenticationService authService)
    {
        _authService = authService;

        SignOutCommand = new RelayCommand(_ => SignOut());
    }

    private void SignOut()
    {
        _authService.Logout();
        var loginWindow = App.GetService<LoginWindow>();
        loginWindow.Show();
        Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault()?.Close();
    }
}
