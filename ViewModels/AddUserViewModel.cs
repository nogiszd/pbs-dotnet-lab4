using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.Validators;

namespace WinLab4.ViewModels;

public class AddUserViewModel : BaseViewModel
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _username = string.Empty;
    private string _email = string.Empty;

    private string _emailErrorMessage = string.Empty;

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
            ValidateEmail();
            OnPropertyChanged(nameof(Email));
        }
    }

    public string EmailErrorMessage
    {
        get => _emailErrorMessage;
        set
        {
            _emailErrorMessage = value;
            OnPropertyChanged(nameof(EmailErrorMessage));
        }
    }

    public ICommand AddUserCommand { get; }

    public AddUserViewModel(IRepository<User> userRepository)
    {
        AddUserCommand = new AddUserCommand(userRepository, this);
    }

    private void ValidateEmail()
    {
        EmailErrorMessage = !string.IsNullOrEmpty(Email) && !EmailValidator.IsValid(Email)
            ? "Adres email jest niepoprawny."
            : string.Empty;
    }
}
