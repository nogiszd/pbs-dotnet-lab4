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

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var loginWindow = App.GetService<LoginWindow>();
        loginWindow.Show();
    }
}
