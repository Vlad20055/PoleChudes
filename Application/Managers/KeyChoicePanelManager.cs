using Domain.Entities;

namespace Application.Managers;

public class KeyChoicePanelManager
{
    private KeyChoicePanel _keyChoicePanel;

    public KeyChoicePanelManager(KeyChoicePanel keyChoicePanel)
    {
        _keyChoicePanel = keyChoicePanel;
    }

    public void Enable() => _keyChoicePanel.IsVisible = true;
    public void Disable() => _keyChoicePanel.IsVisible = false;
}
