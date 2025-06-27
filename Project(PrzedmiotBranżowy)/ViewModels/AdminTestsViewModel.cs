using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_Core.Services;
using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using System.Collections.ObjectModel;
using System.Windows;
using Project_PrzedmiotBranzowy_.ViewNames;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    public class AdminTestsViewModel : BindableBase, INavigationAware
    {
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

        public AdminTestsViewModel(IApplicationDbContext applicationDbContext,
            IDialogService dialogService)
        {
            _applicationDbContext = applicationDbContext;
            _dialogService = dialogService;

            AddTestCommand = new DelegateCommand(() => AddTest());
            DeleteTestCommand = new DelegateCommand(() => DeleteTest(SelectedTest!));
            SaveChangesCommand = new DelegateCommand(() => SaveChanges());
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
