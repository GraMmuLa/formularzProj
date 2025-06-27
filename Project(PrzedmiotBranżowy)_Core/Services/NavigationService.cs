using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy_Core.Services
{
    public class NavigationService : INavigationService
    {

        private readonly IRegionManager _regionManager;

        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool CanNavigate(string regionName)
        {
            return _regionManager.Regions.ContainsRegionWithName(regionName);
        }

        public void NavigateTo(string regionName, string viewName, NavigationParameters? parameters = null)
        {
            _regionManager.RequestNavigate(regionName, viewName, parameters);
        }
    }
}
