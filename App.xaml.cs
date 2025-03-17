using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using WinLab4.Infrastructure.Persistence;
using WinLab4.Infrastructure.Repositories;
using WinLab4.Infrastructure.Services;
using WinLab4.Views;

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
             .AddClasses(c => c.Where(x => x.Name.EndsWith("ViewModel")))
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

        var mainWindow = GetService<LoginWindow>();
        mainWindow.Show();
    }
}
