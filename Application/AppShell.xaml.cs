using Application.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Plugin.BLE;

namespace Application
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Pages.Editor), typeof(Pages.Editor));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            CurrentItem = MainPage;
        }
    }
}
