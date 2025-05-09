using PoleChudes.Domain.Entities;

namespace PoleChudes.UseCases;

public class KeyPanelManager
{
    public KeyPanel KeyPanel { get; set; }
    public event Action? KeySelected;

    public KeyPanelManager()
    {
        KeyPanel = ConstructKeyPanel();
    }

    public void SelectKey(char number)
    {
        foreach (var el in KeyPanel.KeyUnits)
        {
            if (el.Number == number)
            {
                el.Color = "Gold";
                el.Scale = 1.5f;
                break;
            }
        }
        KeySelected?.Invoke();
    }

    public KeyPanel ConstructKeyPanel()
    {
        List<KeyUnit> units = new List<KeyUnit>();
        for (int i = 1; i <= 3; ++i)
        {
            units.Add(new KeyUnit(i.ToString()[0]));
        }
        KeyPanel keyPanel = new KeyPanel(units);
        return keyPanel;
    }
    public void Enable()
    {
        KeyPanel.IsVisible = true;
    }
    public void SetDefaultState()
    {
        foreach (var el in KeyPanel.KeyUnits)
        {
            el.Scale = 1f;
            el.Color = "LightGrey";
        }
    }
    public void Disable()
    {
        KeyPanel.IsVisible = false;
    }
}
