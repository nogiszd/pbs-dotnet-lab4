using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.ViewModels.AdminContext.Pages;

namespace WinLab4.Commands.AdminContext;

public class AcceptReservationCommand(IRepository<Reservation> reservationRepository, Action refreshList, ReservationsViewModel viewModel) 
    : AsyncRelayCommand(async (param) => await ExecuteAcceptReservation(reservationRepository, refreshList, viewModel),
        (param) => CanExecuteAcceptReservation(viewModel))
{
    private static async Task ExecuteAcceptReservation(IRepository<Reservation> repository, Action refreshList, ReservationsViewModel vm)
    {
        if (vm.SelectedReservation == null) return;

        var result = MessageBox.Show(
            $"Czy na pewno chcesz zaakceptować zapis na wydarzenie {vm.SelectedReservation.Event?.Name}?",
            "Akceptowanie zapisu",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
            );

        if (result != MessageBoxResult.Yes) return;

        var reservation = await repository.Get(vm.SelectedReservation.Id);

        reservation.ChangeAcceptation(true);

        try
        {
            await repository.Update(reservation);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas akceptowania zapisu!\nBłąd: {ex.Message}", "Błąd akceptowania zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        refreshList?.Invoke();
    }

    private static bool CanExecuteAcceptReservation(ReservationsViewModel vm) => vm.CanAcceptReservation;
}
