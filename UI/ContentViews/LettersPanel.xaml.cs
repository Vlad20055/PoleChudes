namespace UI.ContentViews;

public partial class LettersPanel : ContentView
{
    public Action<char>? LetterSelected;

	public LettersPanel()
	{
		InitializeComponent();
	}

    private void Letter_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            char letter = button.Text[0];
            LetterSelected?.Invoke(letter);
        }
    }
}