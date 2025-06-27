using Project_PrzedmiotBranzowy_.Views;

namespace Project_PrzedmiotBranzowy_.ViewNames
{
    public static class ViewNamesNavigation
    {
        public static string ContentRegion { get; set; } = "ContentRegion";

        public static string HomeViewName { get; } = nameof(HomeView);
        public static string AdminLoginViewName { get; } = nameof(AdminLoginView);
        public static string AdminRegisterViewName { get; } = nameof(AdminRegisterView);
        public static string AdminTestsViewName { get; } = nameof(AdminTestsView);
        public static string StartTestViewName { get; } = nameof(StartTestView);
        public static string TestViewName { get; } = nameof(TestView);
    }
}
