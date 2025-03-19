using System.Windows.Input;

using WinLab4.Commands.AdminContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.Validators;

namespace WinLab4.ViewModels.AdminContext;

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
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            ValidateEmail();
            OnPropertyChanged();
        }
    }

    public string EmailErrorMessage
    {
        get => _emailErrorMessage;
        set
        {
            _emailErrorMessage = value;
            OnPropertyChanged();
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
