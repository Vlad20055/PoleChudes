namespace UI.ContentViews;

public partial class WordInputPanel : ContentView
{
    public event Action? Refused;
    public event Action<string>? WordClaimed;

	public WordInputPanel()
	{
		InitializeComponent();
	}

    private void ClaimButton_Clicked(object sender, EventArgs e)
    {
        WordClaimed?.Invoke(WordEntry.Text);
    }

    private void RefuseButton_Clicked(object sender, EventArgs e)
    {
        Refused?.Invoke();
    }
}