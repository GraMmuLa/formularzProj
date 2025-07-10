using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_PrzedmiotBranzowy_.ViewModels
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

        private Test? _selectedTest;
        public Test? SelectedTest
        {
            get { return _selectedTest; }
            set {
                SetProperty(ref _selectedTest, value);

                DeleteTestCommand?.RaiseCanExecuteChanged();
                EditTestCommand?.RaiseCanExecuteChanged();
            }
        }

        public NavigationCommand NavigateHomeViewCommand { get; private set; }
        public DelegateCommand AddTestCommand { get; private set; }
        public DelegateCommand<Test> DeleteTestCommand { get; private set; }
        public DelegateCommand<Test> EditTestCommand { get; private set; }
        public DelegateCommand SaveChangesCommand { get; private set; }

        public AdminTestsViewModel(INavigationService navigationService,
            IDialogService dialogService,
            IApplicationDbContext applicationDbContext)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _applicationDbContext = applicationDbContext;

            NavigateHomeViewCommand = new(_navigationService, ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.HomeViewName);

            AddTestCommand = new(AddTest);
            DeleteTestCommand = new(DeleteTest, CanDeleteTest);
            EditTestCommand = new(EditTest, CanEditTest);
            SaveChangesCommand = new(SaveChanges);
        }
        private void AddTest()
        {
            _dialogService.ShowDialog(ViewNamesDialogs.TestDialogViewName, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Test test = result.Parameters.GetValue<Test>("test");

                    test.Admin = _currentAdmin!;

                    Tests.Add(test);
                    _applicationDbContext.Tests.Add(test);
                }
            });
        }

        private bool CanDeleteTest(Test test) => test != null;
        private bool CanEditTest(Test test) => test != null;

        private void EditTest(Test test)
        {
            DialogParameters parameters = new()
            {
                { "test", test }
            };

            _dialogService.ShowDialog(ViewNamesDialogs.TestDialogViewName, parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    Test testResult = result.Parameters.GetValue<Test>("test");

                    Tests.Remove(testResult);
                    Tests.Add(testResult);

                    Test test = _applicationDbContext.Tests.First(x => x.Id == testResult.Id);
                    test = testResult;
                }
            });
        }

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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string username = navigationContext.Parameters.GetValue<string>("username");

            _currentAdmin = _applicationDbContext.Admins.FirstOrDefault(
                (x) => x.Username == navigationContext.Parameters.GetValue<string>("username"));

            Tests = new ObservableCollection<Test>(_currentAdmin!.Tests);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        { }
    }
}
