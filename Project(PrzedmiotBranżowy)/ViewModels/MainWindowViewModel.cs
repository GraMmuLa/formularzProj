using Project_PrzedmiotBranżowy_.Services;
using System.ComponentModel;

namespace Project_PrzedmiotBranżowy_.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
