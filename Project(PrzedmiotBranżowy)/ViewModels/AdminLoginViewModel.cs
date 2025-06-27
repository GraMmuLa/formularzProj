using Project_PrzedmiotBranzowy_.ViewNames;
using Project_PrzedmiotBranzowy_Core.Helpers;
using Project_PrzedmiotBranzowy_Core.Services;

namespace Project_PrzedmiotBranzowy_.ViewModels
{
    internal class AdminLoginViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISecurityService _securityService;

        private string? _username;
        public string? Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public DelegateCommand LoginCommand { get; private set; }

        public NavigationCommand NavigateHomeViewCommand { get; private set; }

        public AdminLoginViewModel(INavigationService navigationService, ISecurityService securityService)
        {
            _navigationService = navigationService;
            _securityService = securityService;
            
            LoginCommand = new DelegateCommand(() => Login());

            NavigateHomeViewCommand = new NavigationCommand(navigationService,
                ViewNamesNavigation.ContentRegion,
                ViewNamesNavigation.HomeViewName);
        }

        private void Login()
        {
            LoginCodes loginResult = _securityService.Login(Username, new string(Password?.Reverse()?.ToArray()));
            NavigationParameters parameters = new()
            {
                { "username", Username ?? string.Empty }
            };

            Username = Password = string.Empty;

            if (loginResult == LoginCodes.OK)
                _navigationService.NavigateTo(ViewNamesNavigation.ContentRegion,
                    ViewNamesNavigation.AdminTestsViewName,
                    parameters);
        }
    }
}
