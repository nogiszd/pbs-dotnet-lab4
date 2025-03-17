using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels;

namespace WinLab4.Commands;

public class RegisterCommand(IRepository<User> userRepository, 
                             RegisterViewModel viewModel) 
    : AsyncRelayCommand(async (param) => await ExecuteRegister(userRepository, viewModel), 
        (param) => CanExecuteRegister(viewModel))
{
    private static async Task ExecuteRegister(IRepository<User> repo, RegisterViewModel vm)
    {
        if (await repo.Exists(x => x.Username == vm.Username && x.Email == vm.Email))
        {
            MessageBox.Show("Użytkownik o podanym loginie lub adresie email już istnieje!", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        string hashedPassword = PasswordHashing.HashPassword(vm.Password);

        var user = new User(vm.FirstName, vm.LastName, vm.Username, hashedPassword, vm.Email);

        await repo.Add(user);

        MessageBox.Show("Rejestracja zakończona pomyślnie!", "Rejestracja", MessageBoxButton.OK);
    }

    private static bool CanExecuteRegister(RegisterViewModel vm)
    {
        return !string.IsNullOrWhiteSpace(vm.FirstName)
            && !string.IsNullOrWhiteSpace(vm.LastName)
            && !string.IsNullOrWhiteSpace(vm.Username) 
            && !string.IsNullOrWhiteSpace(vm.Email) 
            && !string.IsNullOrWhiteSpace(vm.Password)
            && !string.IsNullOrWhiteSpace(vm.ConfirmPassword);
    }
}
