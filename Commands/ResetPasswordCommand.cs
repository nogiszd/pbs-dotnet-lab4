using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels;

namespace WinLab4.Commands;

public class ResetPasswordCommand(IRepository<User> userRepository, UsersViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteResetPassword(userRepository, viewModel),
        (param) => CanExecuteLogin(viewModel))
{
    private static async Task ExecuteResetPassword(IRepository<User> repository, UsersViewModel vm)
    {
        if (vm.SelectedUser == null) return;

        var result = MessageBox.Show(
            $"Czy na pewno chcesz zresetować hasło użytkownika {vm.SelectedUser?.Username}?",
            "Resetowanie hasła",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
            );

        if (result != MessageBoxResult.Yes) return;

        var selectedUser = await repository.Get(vm.SelectedUser!.Id);

        selectedUser.SetPassword(PasswordHashing.HashPassword("reset"));
        selectedUser.EnforcePasswordChange();
        selectedUser.ResetFailedLoginAttempts();

        await repository.Update(selectedUser);

        MessageBox.Show("Hasło zostało zresetowane.\nAby użytkownik mógł ustawić nowe hasło, przekaż mu aby zalogował się z hasłem: 'reset'.", "Resetowanie hasła", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private static bool CanExecuteLogin(UsersViewModel vm)
    {
        return vm.IsUserSelected;
    }
}
