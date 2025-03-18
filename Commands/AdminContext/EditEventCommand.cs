using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.ViewModels.AdminContext;
using WinLab4.Views.AdminContext;

namespace WinLab4.Commands.AdminContext;

public class EditEventCommand(IRepository<Event> eventRepository, EditEventViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteEditEvent(eventRepository, viewModel),
        (param) => CanExecuteEditEvent(viewModel))
{
    private static async Task ExecuteEditEvent(IRepository<Event> repository, EditEventViewModel vm)
    {
        var @event = await repository.Get(vm.Id);

        @event.Update(vm.Name, vm.Agenda, vm.Date!.Value);

        try
        {
            await repository.Update(@event);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas edycji wydarzenia!\nBłąd: {ex.Message}", "Błąd edycji wydarzenia", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Wydarzenie zostało zaktualizowane!", "Edycja wydarzenia", MessageBoxButton.OK, MessageBoxImage.Information);

        Application.Current.Windows.OfType<EditEventWindow>().FirstOrDefault()?.Close();
    }

    private static bool CanExecuteEditEvent(EditEventViewModel vm)
    {
        return !string.IsNullOrWhiteSpace(vm.Name)
            && !string.IsNullOrWhiteSpace(vm.Agenda)
            && vm.Date.HasValue;
    }
}
