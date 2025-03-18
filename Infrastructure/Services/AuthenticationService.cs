using WinLab4.Models;

namespace WinLab4.Infrastructure.Services;

public class AuthenticationService
{
    private User? _currentUser;

    public event Action<User?>? CurrentUserChanged;

    public User? CurrentUser
    {
        get => _currentUser;
        private set
        {
            _currentUser = value;
            CurrentUserChanged?.Invoke(_currentUser);
        }
    }

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
