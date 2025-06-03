namespace UI.ContentViews;

public partial class MenuPanel : ContentView
{
    public event Action? RotateClicked;
    public event Action? WordClicked;

    public MenuPanel()
	{
		InitializeComponent();
	}

    private void WordButton_Clicked(object sender, EventArgs e)
    {
        WordClicked?.Invoke();
    }

    private void RotateButton_Clicked(object sender, EventArgs e)
    {
        RotateClicked?.Invoke();
    }
}