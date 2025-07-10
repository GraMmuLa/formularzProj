using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy__Core.Helpers;
using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_Core.Services;
using System.Collections.ObjectModel;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApplicationDbContext _dbContext;

        private ObservableCollection<ListBoxModel<Test>> _tests;

        public ObservableCollection<ListBoxModel<Test>> Tests
        {
            get { return _tests; }
            set
            {
                SetProperty(ref _tests, value);
            }
        }

        private ListBoxModel<Test> _selectedTest;
        public ListBoxModel<Test> SelectedTest
        {
            get { return _selectedTest; }
            set { SetProperty(ref _selectedTest, value); }
        }

        public NavigationCommand NavigateStartTestCommand { get; private set; }
        public NavigationCommand NavigateAdminLoginCommand { get; private set; }
        public NavigationCommand NavigateAdminRegisterCommand { get; private set; }

        public HomeViewModel(INavigationService navigationService, IApplicationDbContext dbContext) {
            _navigationService = navigationService;
            _dbContext = dbContext;

            Tests = new();
            foreach (var test in _dbContext.Tests)
                Tests.Add(new(test));

            NavigateStartTestCommand = new NavigationCommand(_navigationService,
                ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.StartTestViewName,
                () => new NavigationParameters() { { "test", SelectedTest.Value } });
            NavigateAdminLoginCommand = new NavigationCommand(_navigationService,
                ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.AdminLoginViewName);
            NavigateAdminRegisterCommand = new NavigationCommand(_navigationService,
                ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.AdminRegisterViewName);
        }

        public void StartTest()
        {
            
        }
    }
}
