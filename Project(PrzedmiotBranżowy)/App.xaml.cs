using Microsoft.Extensions.DependencyInjection;
using Project_PrzedmiotBranżowy_.DAL;
using Project_PrzedmiotBranżowy_.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Project_PrzedmiotBranżowy_;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceCollection services = new();

        services.AddDbContext<ApplicationDbContext>();

        services.AddTransient<MainWindowViewModel>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        MainWindow mainWindow = new MainWindow
        {
            DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
        };
    }

}

