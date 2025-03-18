using System.Windows;

using WinLab4.Infrastructure.Repositories;
using WinLab4.Models;
using WinLab4.ViewModels;
using WinLab4.Views;

namespace WinLab4.Commands;

public class AddEventCommand(IRepository<Event> eventRepository, AddEventViewModel viewModel)
    : AsyncRelayCommand(async (param) => await ExecuteAddEvent(eventRepository, viewModel),
        (param) => CanExecuteAddEvent(viewModel))
{
    private static async Task ExecuteAddEvent(IRepository<Event> repository, AddEventViewModel vm)
    {
        var @event = new Event(vm.Name, vm.Agenda, vm.Date!.Value);

        try
        {
            await repository.Add(@event);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas dodawania wydarzenia!\nBłąd: {ex.Message}", "Błąd dodawania wydarzenia", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Application.Current.Windows.OfType<AddEventWindow>().FirstOrDefault()?.Close();
    }

    private static bool CanExecuteAddEvent(AddEventViewModel vm)
    {
        return !string.IsNullOrWhiteSpace(vm.Name) && !string.IsNullOrWhiteSpace(vm.Agenda) && vm.Date.HasValue;
    }
}
