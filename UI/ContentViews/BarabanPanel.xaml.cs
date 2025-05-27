namespace UI.ContentViews;

public partial class BarabanPanel : ContentView
{
	public event Action? SpinClicked;

	public BarabanPanel()
	{
		InitializeComponent();
	}

	public ContentView BarabanContainer => BarabanView;

    private void OnSpinClicked(object sender, EventArgs e)
    {
		SpinClicked?.Invoke();
    }

}