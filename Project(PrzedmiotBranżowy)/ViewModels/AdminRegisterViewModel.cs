using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_Core.Services;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    class AdminRegisterViewModel : BindableBase
    {
        private readonly ISecurityService _securityService;
        private readonly INavigationService _navigationService;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _repeatPassword;
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { SetProperty(ref _repeatPassword, value); }
        }

        public DelegateCommand RegisterCommand { get; set; }
        public NavigationCommand NavigateHomeViewCommand { get; set; }

        public AdminRegisterViewModel(ISecurityService securityService,
            INavigationService navigationService)
        {
            _securityService = securityService;
            _navigationService = navigationService;

            RegisterCommand = new DelegateCommand(() => Register());

            NavigateHomeViewCommand = new NavigationCommand(_navigationService,
                ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.HomeViewName);
        }

        public void Register()
        {
            LoginCodes registerResult = _securityService.Register(Username,
                new string(Password?.Reverse()?.ToArray()),
                new string(RepeatPassword.Reverse()?.ToArray()));
            NavigationParameters parameters = new()
            {
                { "username", Username ?? string.Empty }
            };

            Username = Password = RepeatPassword = string.Empty;

            if (registerResult == LoginCodes.OK)
                _navigationService.NavigateTo(ViewNamesNavigation.ContentRegion,
                    ViewNamesNavigation.AdminTestsViewName,
                    parameters);
        }
    }
}
