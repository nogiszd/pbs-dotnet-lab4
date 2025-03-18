using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.ViewModels.AdminContext;
using WinLab4.Views.AdminContext;

namespace WinLab4.Commands.AdminContext;

public class AddUserCommand(IRepository<User> userRepository, AddUserViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteAddUser(userRepository, viewModel),
        (param) => CanExecuteAddUser(viewModel))
{
    private static async Task ExecuteAddUser(IRepository<User> repository, AddUserViewModel vm)
    {
        if (await repository.Exists(x => x.Username == vm.Username || x.Email == vm.Email))
        {
            MessageBox.Show("Użytkownik o podanym loginie lub adresie email już istnieje!", "Błąd dodawania użytkownika", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        var user = new User(vm.FirstName, vm.LastName, vm.Username, null, vm.Email);
        user.EnforcePasswordChange();

        try
        {
            await repository.Add(user);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika!\nBłąd: {ex.Message}", "Błąd dodawania użytkownika", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Application.Current.Windows.OfType<AddUserWindow>().FirstOrDefault()?.Close();
    }

    private static bool CanExecuteAddUser(AddUserViewModel vm)
    {
        return !string.IsNullOrWhiteSpace(vm.FirstName)
            && !string.IsNullOrWhiteSpace(vm.LastName)
            && !string.IsNullOrWhiteSpace(vm.Username)
            && !string.IsNullOrWhiteSpace(vm.Email)
            && string.IsNullOrEmpty(vm.EmailErrorMessage); // Email validation
    }
}
