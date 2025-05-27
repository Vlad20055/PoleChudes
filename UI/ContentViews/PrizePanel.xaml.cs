namespace UI.ContentViews;

public partial class PrizePanel : ContentView
{
    public event Action? PrizeSelected;
    public event Action? MoneySelected;

	public PrizePanel()
	{
		InitializeComponent();
	}

    private void MoneyButton_Clicked(object sender, EventArgs e)
    {
        MoneySelected?.Invoke();
    }

    private void PrizeButton_Clicked(object sender, EventArgs e)
    {
        PrizeSelected?.Invoke();
    }
}