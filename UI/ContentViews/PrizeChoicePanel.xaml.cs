namespace UI.ContentViews;

public partial class PrizeChoicePanel : ContentView
{
    public event Action<bool>? Chosen;

    public PrizeChoicePanel()
	{
		InitializeComponent();
	}

    private void PrizeButton_Clicked(object sender, EventArgs e)
    {
        Chosen?.Invoke(true);
    }

    private void RefuseButton_Clicked(object sender, EventArgs e)
    {
        Chosen?.Invoke(false);
    }
}