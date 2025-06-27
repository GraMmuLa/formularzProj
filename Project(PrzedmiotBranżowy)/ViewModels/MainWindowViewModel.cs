using Project_PrzedmiotBranzowy_Core.Services;
using System.ComponentModel;

namespace Project_PrzedmiotBranzowy_.ViewModels
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
