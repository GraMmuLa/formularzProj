using Project_PrzedmiotBranżowy_BackEnd.DAL;
using Project_PrzedmiotBranżowy_.Helpers;
using Project_PrzedmiotBranżowy_BackEnd.Models;
using Project_PrzedmiotBranżowy_.Services;
using Project_PrzedmiotBranżowy_.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_PrzedmiotBranżowy_.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApplicationDbContext _dbContext;

        private ObservableCollection<Test> _tests;

        public ObservableCollection<Test> Tests
        {
            get { return _tests; }
            set
            {
                SetProperty(ref _tests, value);
            }
        }

        public NavigationCommand NavigateAdminLoginCommand { get; private set; }

        public HomeViewModel(INavigationService navigationService, IApplicationDbContext dbContext) {
            _navigationService = navigationService;
            _dbContext = dbContext;

            Tests = new ObservableCollection<Test>(_dbContext.Tests);

            NavigateAdminLoginCommand = new NavigationCommand(_navigationService, "ContentRegion", ViewNamesNavigation.AdminLoginView);
        }
    }
}
