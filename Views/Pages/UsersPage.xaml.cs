using System.Windows.Controls;

using WinLab4.ViewModels;

namespace WinLab4.Views.Pages;

/// <summary>
/// Logika interakcji dla klasy UsersPage.xaml
/// </summary>
public partial class UsersPage : Page
{
    private readonly UsersViewModel _viewModel;

    public UsersPage(UsersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;

        IsVisibleChanged += UsersPage_IsVisibleChanged;
    }

    private async void UsersPage_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            await _viewModel.LoadUsers();
        }
    }
}
