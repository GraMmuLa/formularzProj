namespace Project_PrzedmiotBranzowy_;

using Prism.Ioc;
using Prism.Navigation.Regions;
using Prism.Unity;
using Project_PrzedmiotBranzowy_.ViewModels;
using Project_PrzedmiotBranzowy_.ViewModels.DialogViewModels;
using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_.Views;
using Project_PrzedmiotBranzowy_.Views.Dialogs;
using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_Core.Services;
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

        containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(ViewNamesNavigation.HomeViewName);
        containerRegistry.RegisterForNavigation<AdminLoginView, AdminLoginViewModel>(ViewNamesNavigation.AdminLoginViewName);
        containerRegistry.RegisterForNavigation<AdminRegisterView, AdminRegisterViewModel>(ViewNamesNavigation.AdminRegisterViewName);
        containerRegistry.RegisterForNavigation<AdminTestsView, AdminTestsViewModel>(ViewNamesNavigation.AdminTestsViewName);
        containerRegistry.RegisterForNavigation<StartTestView, StartTestViewModel>(ViewNamesNavigation.StartTestViewName);
        containerRegistry.RegisterForNavigation<TestView, TestViewModel>(ViewNamesNavigation.StartTestViewName);

        containerRegistry.RegisterDialog<TestDialogView, TestDialogViewModel>(ViewNamesDialogs.TestDialogViewName);
        containerRegistry.RegisterDialog<QuestionDialogView, QuestionDialogViewModel>(ViewNamesDialogs.QuestionDialogViewName);
        containerRegistry.RegisterDialog<AnswerDialogView, AnswerDialogViewModel>(ViewNamesDialogs.AnswerDialogViewName);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        IRegionManager regionManager = Container.Resolve<IRegionManager>();
        regionManager.RequestNavigate(ViewNamesNavigation.ContentRegion, nameof(HomeView));
    }
    protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
    {
        base.RegisterRequiredTypes(containerRegistry);

        containerRegistry.RegisterSingleton<INavigationService, NavigationService>();
        containerRegistry.RegisterSingleton<ISecurityService, SecurityService>();
        containerRegistry.RegisterSingleton<IApplicationDbContext, ApplicationDbContext>();
    }
}

