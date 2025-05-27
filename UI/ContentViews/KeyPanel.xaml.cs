namespace UI.ContentViews;

public partial class KeyPanel : ContentView
{
	public event Action<char>? KeySelected;

	public KeyPanel()
	{
		InitializeComponent();
	}

    private void Key_Clicked(object sender, EventArgs e)
    {
		if (sender is Button btn) KeySelected?.Invoke(btn.Text[0]);
    }
}