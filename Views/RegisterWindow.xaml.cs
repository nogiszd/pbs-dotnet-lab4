using System.Windows;

using WinLab4.ViewModels;

namespace WinLab4.Views;

/// <summary>
/// Logika interakcji dla klasy RegisterWindow.xaml
/// </summary>
public partial class RegisterWindow : Window
{
    public RegisterWindow(RegisterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
