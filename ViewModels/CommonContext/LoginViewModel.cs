using System.Windows.Input;

using WinLab4.Commands.CommonContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;

namespace WinLab4.ViewModels.CommonContext;

public class LoginViewModel : BaseViewModel
{
    private string _login = string.Empty;
    private string _password = string.Empty;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(IRepository<User> userRepository, AuthenticationService authenticationService)
    {
        LoginCommand = new LoginCommand(userRepository, authenticationService, this);
    }
}
