using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels.CommonContext;
using WinLab4.Views.CommonContext;

namespace WinLab4.Commands.CommonContext;

public class NewPasswordCommand(IRepository<User> userRepository, AuthenticationService authService, NewPasswordViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteNewPassword(userRepository, authService, viewModel),
        (param) => CanExecuteNewPassword(viewModel))
{
    private static async Task ExecuteNewPassword(IRepository<User> repository, AuthenticationService service, NewPasswordViewModel vm)
    {
        if (service.CurrentUser == null) return;

        var user = await repository.Get(service.CurrentUser.Id);

        string hashedPassword = PasswordService.HashPassword(vm.Password);

        user.SetPassword(hashedPassword);

        try
        {
            await repository.Update(user);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas zmiany hasła!\nBłąd: {ex.Message}", "Błąd ustawiania hasła", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Hasło zostało zmienione pomyślnie!", "Zmiana hasła", MessageBoxButton.OK);

        Application.Current.Windows.OfType<NewPasswordWindow>().FirstOrDefault()?.Close();
    }

    private static bool CanExecuteNewPassword(NewPasswordViewModel vm)
    {
        return !string.IsNullOrWhiteSpace(vm.Password)
            && !string.IsNullOrWhiteSpace(vm.ConfirmPassword)
            && string.IsNullOrEmpty(vm.PasswordErrorMessage);
    }
}
