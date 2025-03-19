using System.Windows;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.ViewModels.UserContext.Pages;

namespace WinLab4.Commands.UserContext;

public class NewReservationCommand(IRepository<Reservation> reservationRepository, AuthenticationService authService, NewReservationViewModel viewModel, Action refreshList) 
    : AsyncRelayCommand(async (param) => await ExecuteNewReservation(reservationRepository, authService, refreshList, viewModel),
        (param) => CanExecuteNewReservation(viewModel))
{
    private static async Task ExecuteNewReservation(IRepository<Reservation> repository, AuthenticationService service, Action refreshList, NewReservationViewModel vm)
    {
        var user = service.CurrentUser;

        if (user == null) return;

        var reservation = new Reservation(
            user.Id,
            vm.SelectedEvent!.Id,
            vm.SelectedParticipationType!.Value,
            vm.SelectedCateringPreference!.Value
            );

        try
        {
            await repository.Add(reservation);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas zapisu na wydarzenie.\nBłąd: {ex.Message}", "Błąd zapisu na wydarzenie", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        vm.SelectedCateringPreference = null;
        vm.SelectedParticipationType = null;
        vm.SelectedEvent = null;

        MessageBox.Show("Zapisano na wydarzenie!", "Zapis na wydarzenie", MessageBoxButton.OK, MessageBoxImage.Information);
        refreshList?.Invoke();
    }

    private static bool CanExecuteNewReservation(NewReservationViewModel vm)
    {
        return vm.SelectedCateringPreference != null 
            && vm.SelectedEvent != null 
            && vm.SelectedParticipationType != null;
    }
}
