using Project_PrzedmiotBranżowy_.Helpers;
using Project_PrzedmiotBranżowy_.Services;
using Project_PrzedmiotBranżowy_.Views;

namespace Project_PrzedmiotBranżowy_.ViewModels
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

            NavigateHomeViewCommand = new NavigationCommand(navigationService, "ContentRegion", nameof(HomeView));
        }

        private void Login()
        {
            LoginCodes loginResult = _securityService.Login(Username, new string(Password?.Reverse()?.ToArray()));
            if (loginResult == LoginCodes.OK)
                _navigationService.NavigateTo("ContentRegion", "HomeView");
        }
    }
}
