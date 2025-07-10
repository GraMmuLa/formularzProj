using Project_PrzedmiotBranzowy_Core.Services;

namespace Project_PrzedmiotBranzowy_Core.Helpers
{
    public class NavigationCommand : DelegateCommand
    {
        public NavigationCommand(INavigationService navigationService,
            string regionName,
            string viewName,
            Func<NavigationParameters>? parametersFactory = null)
            : base(() =>
            {
                NavigationParameters parameters = parametersFactory?.Invoke() ?? new();
                navigationService.NavigateTo(regionName, viewName, parameters);
            })
        {
        }
    }
}
