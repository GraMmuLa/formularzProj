namespace Project_PrzedmiotBranżowy_;

using Prism.Ioc;
using Prism.Navigation.Regions;
using Prism.Unity;
using Project_PrzedmiotBranżowy_.DAL;
using Project_PrzedmiotBranżowy_.Services;
using Project_PrzedmiotBranżowy_.Views;
using System.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomeView>(nameof(HomeView));
        containerRegistry.RegisterForNavigation<AdminLoginView>(nameof(AdminLoginView));
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        IRegionManager regionManager = Container.Resolve<IRegionManager>();
        regionManager.RequestNavigate("ContentRegion", nameof(HomeView));
    }
    protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
    {
        base.RegisterRequiredTypes(containerRegistry);

        containerRegistry.RegisterSingleton<INavigationService, NavigationService>();
        containerRegistry.RegisterSingleton<ISecurityService, SecurityService>();
        containerRegistry.RegisterSingleton<ApplicationDbContext>();
    }
}

