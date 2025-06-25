using Project_PrzedmiotBranżowy_.Helpers;
using Project_PrzedmiotBranżowy_.Services;
using Project_PrzedmiotBranżowy_BackEnd.DAL;
using Project_PrzedmiotBranżowy_BackEnd.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_PrzedmiotBranżowy_.ViewModels
{
    public class AdminTestsViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDialogService _dialogService;

        private Admin? _currentAdmin = null;

        private ObservableCollection<Test> _tests;
        public ObservableCollection<Test> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }

        private Test? _selectedTest = null;

        public Test? SelectedTest
        {
            get => _selectedTest;
            set
            {
                SetProperty(ref _selectedTest, value);
                IsSelectedTest = value != null;
            }
        }

        private bool _isSelectedTest = false;
        public bool IsSelectedTest
        {
            get => _isSelectedTest;
            set => SetProperty(ref _isSelectedTest, value);
        }

        public DelegateCommand AddTestCommand { get; private set; }
        public DelegateCommand DeleteTestCommand { get; private set; }
        public DelegateCommand EditTestCommand { get; private set; }
        public DelegateCommand SaveChangesCommand { get; private set; }

        public AdminTestsViewModel(INavigationService navigationService,
            IApplicationDbContext applicationDbContext,
            IDialogService dialogService) {
            _applicationDbContext = applicationDbContext;
            _navigationService = navigationService;
            _dialogService = dialogService;

            Tests = new ObservableCollection<Test>(_applicationDbContext.Tests);

            AddTestCommand = new DelegateCommand(() => AddTest());
            DeleteTestCommand = new DelegateCommand(() => DeleteTest(SelectedTest!));
            SaveChangesCommand = new DelegateCommand(() => SaveChanges());
        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string username = navigationContext.Parameters.GetValue<string>("username");

            _currentAdmin = _applicationDbContext.Admins.FirstOrDefault(
                (x) => x.Username == navigationContext.Parameters.GetValue<string>("username"));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        { }
    
    
        private void DeleteTest(Test test)
        {
            Tests.Remove(test);
            _applicationDbContext.Tests.Remove(test);
            SelectedTest = null;
        }

        private void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
            MessageBox.Show("Зміни збережено", "Інформація", MessageBoxButton.OK);
        }

        private void AddTest()
        {
            _dialogService.ShowDialog(ViewNamesDialogs.AddTestDialogViewName, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Test test = result.Parameters.GetValue<Test>("test");

                    test.Admin = _currentAdmin!;

                    Tests.Add(test);
                    _applicationDbContext.Tests.Add(test);
                    _applicationDbContext.SaveChanges();

                }
            });
        }
    }
}
