using System.Windows;

using WinLab4.ViewModels.AdminContext;

namespace WinLab4.Views.AdminContext;

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
