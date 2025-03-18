using System.Windows;

using WinLab4.ViewModels.AdminContext;

namespace WinLab4.Views.AdminContext;

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
