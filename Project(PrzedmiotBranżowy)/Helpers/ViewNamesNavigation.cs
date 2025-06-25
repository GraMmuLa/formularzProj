using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranżowy_.Helpers
{
    public static class ViewNamesNavigation
    {
        public static string HomeView { get; set; } = nameof(Views.HomeView);
        public static string AdminLoginView { get; set; } = nameof(Views.AdminLoginView);
        public static string AdminTestsView { get; set; } = nameof(Views.AdminTestsView);
    }
}
