using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.ViewModels.AdminContext.Pages;

namespace WinLab4.Commands.AdminContext;

public class RejectReservationCommand(IRepository<Reservation> reservationRepository, Action refreshList, ReservationsViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteRejectReservation(reservationRepository, refreshList, viewModel),
        (param) => CanExecuteRejectReservation(viewModel))
{
    private static async Task ExecuteRejectReservation(IRepository<Reservation> repository, Action refreshList, ReservationsViewModel vm)
    {
        if (vm.SelectedReservation == null) return;

        var result = MessageBox.Show(
            $"Czy na pewno chcesz odrzucić zapis na wydarzenie {vm.SelectedReservation.Event?.Name}?",
            "Resetowanie hasła",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
            );

        if (result != MessageBoxResult.Yes) return;

        var reservation = await repository.Get(vm.SelectedReservation.Id);

        reservation.ChangeAcceptation(false);

        try
        {
            await repository.Update(reservation);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas odrzucenia zapisu!\nBłąd: {ex.Message}", "Błąd odrzucenia zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        refreshList?.Invoke();
    }

    private static bool CanExecuteRejectReservation(ReservationsViewModel vm) => vm.CanRejectReservation;
}
