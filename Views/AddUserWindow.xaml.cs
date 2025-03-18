using System.Windows;

using WinLab4.ViewModels;

namespace WinLab4.Views;

/// <summary>
/// Logika interakcji dla klasy AddUserWIndow.xaml
/// </summary>
public partial class AddUserWindow : Window
{
    public AddUserWindow(AddUserViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
