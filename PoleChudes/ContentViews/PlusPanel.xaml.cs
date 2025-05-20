namespace PoleChudes.ContentViews;

public partial class PlusPanel : ContentView
{
	public event Action<int>? PositionSelected;

	public PlusPanel()
	{
		InitializeComponent();
	}

    private void OnPositionSelected(object sender, EventArgs e)
    {
		if (sender is Picker picker && picker.SelectedItem is int number) { PositionSelected?.Invoke(number); }
    }
}