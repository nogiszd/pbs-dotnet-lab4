using System.Windows;

using WinLab4.ViewModels;

namespace WinLab4.Views;

/// <summary>
/// Logika interakcji dla klasy AddEventWindow.xaml
/// </summary>
public partial class AddEventWindow : Window
{
    public AddEventWindow(AddEventViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
