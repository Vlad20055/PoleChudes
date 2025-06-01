namespace UI.ContentViews;

public partial class PrizesSuperGamePanel : ContentView
{
	public event Action<string>? PrizeSelected;
    public event Action? Confirmed;

	public PrizesSuperGamePanel()
	{
		InitializeComponent();
	}

    private void Prize_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            PrizeSelected?.Invoke(button.Text);
        }
    }

    private void Confirm_Clicked(object sender, EventArgs e)
    {
        Confirmed?.Invoke();
    }
}