using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy__Core.Helpers;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using Project_PrzedmiotBranzowy_Core.Services;
using System.Windows;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    internal class StartTestViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private Test _currentTest;

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public DelegateCommand StartTestCommand { get; private set; }

        public StartTestViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartTestCommand = new DelegateCommand(() => StartQuiz());
        }

        public void StartQuiz()
        {
            if (string.IsNullOrEmpty(FirstName)) {
                MessageBox.Show("Введіть ім'я");
                return;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("Введіть прізвище");
                return;
            }

            NavigationParameters parameters = new()
            {
                { "test", _currentTest },
                { "user", new User() { Firstname = this.FirstName, Lastname = this.LastName } }
            };
            _navigationService.NavigateTo(ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.TestViewName,
                parameters);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentTest = navigationContext.Parameters.GetValue<Test>("test");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
