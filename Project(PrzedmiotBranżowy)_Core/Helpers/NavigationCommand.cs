using Microsoft.Identity.Client;
using Project_PrzedmiotBranzowy_Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
