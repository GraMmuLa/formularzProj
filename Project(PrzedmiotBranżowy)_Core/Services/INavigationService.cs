using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy_Core.Services
{
    public interface INavigationService
    {
        public void NavigateTo(string regionName, string viewName, NavigationParameters? parameters = null);
        public bool CanNavigate(string regionName);
    }
}
