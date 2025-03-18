using System.Windows;
using System.Windows.Controls;

using WinLab4.ViewModels.CommonContext;

namespace WinLab4.Views.CommonContext;

/// <summary>
/// Logika interakcji dla klasy LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        PasswordTextBox.Text = PasswordBox.Password;
        PasswordTextBox.Visibility = Visibility.Visible;
        PasswordBox.Visibility = Visibility.Collapsed;
    }

    private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        PasswordBox.Password = PasswordTextBox.Text;
        PasswordTextBox.Visibility = Visibility.Collapsed;
        PasswordBox.Visibility = Visibility.Visible;
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var registerWindow = App.GetService<RegisterWindow>();
        registerWindow.Show();
        Close();
    }
}
