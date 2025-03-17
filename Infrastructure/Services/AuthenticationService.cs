using WinLab4.Models;

namespace WinLab4.Infrastructure.Services;

public class AuthenticationService
{
    private User? _currentUser;

    public User? CurrentUser => _currentUser;

    public bool IsLoggedIn => _currentUser != null;

    public event Action? OnUserChanged;

    public void Login(User user)
    {
        _currentUser = user;
        OnUserChanged?.Invoke();
    }

    public void Logout()
    {
        _currentUser = null;
        OnUserChanged?.Invoke();
    }
}
