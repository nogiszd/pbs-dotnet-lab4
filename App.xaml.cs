using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using WinLab4.Infrastructure.Persistence;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Models;
using WinLab4.Models.Enums;
using WinLab4.Views.AdminContext;
using WinLab4.Views.CommonContext;
using WinLab4.Views.UserContext;

namespace WinLab4;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider ServiceProvider = null!;

    private readonly static string _connectionString = "server=localhost;port=3307;uid=root;pwd=password;database=lab_example;";

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        var authService = GetService<AuthenticationService>();
        authService.CurrentUserChanged += OnUserChange;
    }

    private void OnUserChange(User? user)
    {
        if (user == null) return;

        Current.Dispatcher.Invoke(() =>
        {
            Window? nextWindow = null;

            switch (user.Role)
            {
                case UserRole.User:
                    nextWindow = GetService<MainWindow>();
                    break;
                case UserRole.Admin:
                    nextWindow = GetService<AdminWindow>();
                    break;
                default:
                    return;
            }

            if (user.NeedsNewPassword)
            {
                nextWindow = GetService<NewPasswordWindow>();
                nextWindow?.Show();
                return;
            }

            nextWindow?.Show();

            foreach (Window window in Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Close();
                    break;
                }
            }
        });
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddSingleton<AuthenticationService>();

        services.Scan(x =>
            x.FromAssemblyOf<App>()
             .AddClasses(c => c.AssignableTo<Window>())
             .AsSelf()
             .WithTransientLifetime()
        );

        services.Scan(x =>
            x.FromAssemblyOf<App>()
             .AddClasses(c => c.AssignableTo<Page>())
             .AsSelf()
             .WithTransientLifetime()
        );

        services.Scan(x =>
            x.FromAssemblyOf<App>()
             .AddClasses(c => c.Where(x => x.Name.EndsWith("ViewModel")).Where(x => !x.Name.StartsWith("EditEvent")))
             .AsSelf()
             .WithTransientLifetime()
        );
    }

    public static T GetService<T>() where T : class
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var loginWindow = GetService<LoginWindow>();
        loginWindow.Show();

        Current.Exit += (s, args) =>
        {
            if (Current.Windows.Count == 0)
            {
                Current.Shutdown();
            }
        };
    }
}
