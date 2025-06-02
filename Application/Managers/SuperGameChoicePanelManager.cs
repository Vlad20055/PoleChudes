using Domain.Entities;

namespace Application.Managers;

public class SuperGameChoicePanelManager
{
    private SuperGameChoicePanel _superGameChoicePanel;

    public SuperGameChoicePanelManager(SuperGameChoicePanel superGameChoicePanel)
    {
        _superGameChoicePanel = superGameChoicePanel;
    }

    public void Enable() => _superGameChoicePanel.IsVisible = true;
    public void Disable() => _superGameChoicePanel.IsVisible = false;
}
