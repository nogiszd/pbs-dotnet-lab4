using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels;

namespace WinLab4.Commands;

public class LoginCommand(IRepository<User> userRepository, 
                          AuthenticationService authenticationService, 
                          LoginViewModel viewModel) 
    : AsyncRelayCommand(async (param) => await ExecuteLogin(userRepository, authenticationService, viewModel), 
        (param) => CanExecuteLogin(viewModel))
{
    private static async Task ExecuteLogin(IRepository<User> repository, AuthenticationService service, LoginViewModel vm)
    {
        var user = await repository.TryGet(x => x.Username == vm.Login, null);

        if (user == null || PasswordHashing.VerifyPassword(vm.Password, user.PasswordHash))
        {
            MessageBox.Show("Niepoprawny login lub hasło!", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        service.Login(user);

        // todo: redirect to windows depending on user role
    }

    private static bool CanExecuteLogin(LoginViewModel vm)
    {
        return !string.IsNullOrEmpty(vm.Login) && !string.IsNullOrEmpty(vm.Password);
    }
}
