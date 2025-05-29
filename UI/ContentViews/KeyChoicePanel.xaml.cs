namespace UI.ContentViews;

public partial class KeyChoicePanel : ContentView
{
    public event Action<bool>? Chosen;

	public KeyChoicePanel()
	{
		InitializeComponent();
	}

    private void KeyButton_Clicked(object sender, EventArgs e)
    {
        Chosen?.Invoke(true);
    }
    private void RefuseButton_Clicked(object sender, EventArgs e)
    {
        Chosen?.Invoke(false);
    }
}