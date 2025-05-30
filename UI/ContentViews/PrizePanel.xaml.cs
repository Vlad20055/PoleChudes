namespace UI.ContentViews;

public partial class PrizePanel : ContentView
{
    public event Action<string>? PrizeSelected;

	public PrizePanel()
	{
		InitializeComponent();
	}

    private void MoneyButton_Clicked(object sender, EventArgs e)
    {
        PrizeSelected?.Invoke("money");
    }

    private void PrizeButton_Clicked(object sender, EventArgs e)
    {
        PrizeSelected?.Invoke("prize");
    }
}