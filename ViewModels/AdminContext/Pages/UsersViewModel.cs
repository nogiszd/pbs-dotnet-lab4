using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using WinLab4.Commands;
using WinLab4.Commands.AdminContext;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.Views.AdminContext;

namespace WinLab4.ViewModels.AdminContext.Pages;

public class UsersViewModel : BaseViewModel
{
    private readonly IRepository<User> _userRepository;
    private readonly AuthenticationService _authService;
    private User? _selectedUser;

    public ObservableCollection<User> Users { get; } = new();

    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));   
        }
    }

    public bool IsUserSelected => SelectedUser != null;

    public ICommand AddUserCommand { get; }
    public ICommand ResetUserPasswordCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UsersViewModel(IRepository<User> userRepository, AuthenticationService authService)
    {
        _userRepository = userRepository;
        _authService = authService;

        AddUserCommand = new RelayCommand(_ => AddUser());
        ResetUserPasswordCommand = new ResetPasswordCommand(userRepository, this);
        DeleteUserCommand = new AsyncRelayCommand(async (param) => await DeleteUser(), _ => IsUserSelected);

        _ = LoadUsers();
    }

    public async Task LoadUsers()
    {
        var users = await _userRepository.Find(_ => true);

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Users.Clear();
            foreach (var user in users)
            {
                if (user.Id != _authService.CurrentUser?.Id)
                {
                    Users.Add(user);
                }
            }
        });
    }

    private async void AddUser()
    {
        var addUserWindow = App.GetService<AddUserWindow>();
        addUserWindow.ShowDialog();
        await LoadUsers();
    }

    private async Task DeleteUser()
    {
        if (SelectedUser == null) return;

        var result = MessageBox.Show(
            $"Czy na pewno chcesz usunąć użytkownika {SelectedUser.Username}?",
            "Usuwanie użytkownika",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
            );

        if (result == MessageBoxResult.Yes)
        {
            await _userRepository.Delete(SelectedUser);
            await LoadUsers();
        }
    }
}
