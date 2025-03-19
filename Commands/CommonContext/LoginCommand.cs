using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels.CommonContext;

namespace WinLab4.Commands.CommonContext;

public class LoginCommand(IRepository<User> userRepository,
                          AuthenticationService authenticationService,
                          LoginViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteLogin(userRepository, authenticationService, viewModel),
        (param) => CanExecuteLogin(viewModel))
{
    private static async Task ExecuteLogin(IRepository<User> repository, AuthenticationService service, LoginViewModel vm)
    {
        var user = await repository.TryGet(x => x.Username == vm.Login);

        if (user == null)
        {
            ShowError();
            return;
        }

        if (user.IsLockedOut)
        {
            ShowError("Konto jest zablokowane przez zbyt dużą ilość błędnych prób logowania.");
            return;
        }

        if (!PasswordService.VerifyPassword(vm.Password, user.PasswordHash))
        {
            user.IncrementFailedLoginAttempts();
            await repository.Update(user);
            ShowError();
            return;
        }

        if (user.FailedLoginAttempts > 0)
        {
            user.ResetFailedLoginAttempts();
            await repository.Update(user);
        }

        service.Login(user);
    }

    private static bool CanExecuteLogin(LoginViewModel vm)
    {
        return !string.IsNullOrEmpty(vm.Login) && !string.IsNullOrEmpty(vm.Password);
    }

    private static void ShowError(string message = "Niepoprawny login lub hasło!")
    {
        MessageBox.Show(message, "Błąd autoryzacji", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
