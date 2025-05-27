namespace UI
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var mainPage = new NavigationPage(new LoginPage());
            return new Window(mainPage);
        }
    }
}