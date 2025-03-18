using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;

namespace WinLab4.ViewModels;

public class NewPasswordViewModel : BaseViewModel
{
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;

    private string _passwordErrorMessage = string.Empty;

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            ValidatePasswords();
            OnPropertyChanged(nameof(ConfirmPassword));
        }
    }

    public string PasswordErrorMessage
    {
        get => _passwordErrorMessage;
        set
        {
            _passwordErrorMessage = value;
            OnPropertyChanged(nameof(PasswordErrorMessage));
        }
    }

    public ICommand NewPasswordCommand { get; }

    public NewPasswordViewModel(IRepository<User> userRepository, AuthenticationService authService)
    {
        NewPasswordCommand = new NewPasswordCommand(userRepository, authService, this);
    }

    private void ValidatePasswords()
    {
        if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
        {
            if (Password != ConfirmPassword)
            {
                PasswordErrorMessage = "Hasła nie są zgodne.";
                return;
            }
        }

        PasswordErrorMessage = string.Empty;
    }
}
