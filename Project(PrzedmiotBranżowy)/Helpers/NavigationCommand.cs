using Project_PrzedmiotBranżowy_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.Helpers
{
    public class NavigationCommand : DelegateCommand
    {
        public NavigationCommand(INavigationService navigationService,
            string regionName,
            string viewName,
            NavigationParameters? parameters = null)
            : base(() => navigationService.NavigateTo(regionName, viewName, parameters))
        {
        }

    }
}
