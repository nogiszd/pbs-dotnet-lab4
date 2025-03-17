using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;

namespace WinLab4.ViewModels;

public class RegisterViewModel : INotifyPropertyChanged
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _username = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _passwordErrorMessage = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

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

    public ICommand RegisterCommand { get; }

    public RegisterViewModel(IRepository<User> userRepository)
    {
        RegisterCommand = new RegisterCommand(userRepository, this);
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

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
