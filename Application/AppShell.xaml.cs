using Application.Pages;

namespace Application
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.Editor), typeof(Pages.Editor));
            Routing.RegisterRoute(nameof(Pages.MainPage), typeof(Pages.MainPage));
            Routing.RegisterRoute(nameof(Pages.Settings), typeof(Pages.Settings));
            CurrentItem = MainPage;
        }
    }
}
