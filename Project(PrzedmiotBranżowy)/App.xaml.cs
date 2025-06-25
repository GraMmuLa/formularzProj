namespace Project_PrzedmiotBranżowy_;

using Prism.Ioc;
using Prism.Navigation.Regions;
using Prism.Unity;
using Project_PrzedmiotBranżowy_BackEnd.DAL;
using Project_PrzedmiotBranżowy_.Helpers;
using Project_PrzedmiotBranżowy_.Services;
using Project_PrzedmiotBranżowy_.Views;
using System.Windows;
using Project_PrzedmiotBranżowy_.ViewModels;
using Project_PrzedmiotBranżowy_.Views.Dialogs;
using Project_PrzedmiotBranżowy_.ViewModels.DialogViewModels;

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
        containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNamesNavigation.HomeView);
        containerRegistry.RegisterForNavigation<AdminLoginView, AdminLoginViewModel>(ViewNamesNavigation.AdminLoginView);
        containerRegistry.RegisterForNavigation<AdminTestsView, AdminTestsViewModel>(ViewNamesNavigation.AdminTestsView);

        containerRegistry.RegisterDialog<AddTestDialogView, AddTestDialogViewModel>();
        containerRegistry.RegisterDialog<AddQuestionDialogView, AddQuestionDialogViewModel>();
        containerRegistry.RegisterDialog<AddAnswerDialogView, AddAnswerDialogViewModel>();
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
        containerRegistry.RegisterSingleton<IApplicationDbContext, ApplicationDbContext>();
    }
}

