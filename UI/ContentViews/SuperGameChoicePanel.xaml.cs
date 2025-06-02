namespace UI.ContentViews;

public partial class SuperGameChoicePanel : ContentView
{
	public event Action<bool>? OnChoiceSelected;

	public SuperGameChoicePanel()
	{
		InitializeComponent();
	}

    private void SuperGameButton_Clicked(object sender, EventArgs e)
    {
        OnChoiceSelected?.Invoke(true);
    }

    private void RefuseButton_Clicked(object sender, EventArgs e)
    {
        OnChoiceSelected?.Invoke(false);
    }
}