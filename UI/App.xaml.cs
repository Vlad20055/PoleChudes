namespace UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var loginPage = Handler.MauiContext.Services.GetService<LoginPage>()
                        ?? throw new InvalidOperationException("LoginPage не зарегистрирована в DI");

        var nav = new NavigationPage(loginPage);
        return new Window(nav);
    }
}