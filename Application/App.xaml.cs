namespace Application
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    Window window = base.CreateWindow(activationState);

        //    window.Created += (s, e) =>
        //    {
        //        // Custom logic
        //    };

        //    return window;
        //}
    }
}
