namespace UI;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnPlayButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GamePage());
    }
}