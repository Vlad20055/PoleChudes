namespace UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        LoginPage? loginPage = null;

        if (Handler.MauiContext != null)
        {
            loginPage = Handler.MauiContext.Services.GetService<LoginPage>();
        }

        var nav = new NavigationPage(loginPage);
        return new Window(nav);
    }
}