using Domain.Entities;

namespace Application.Managers;

public class PrizeChoicePanelManager
{
    private PrizeChoicePanel _prizeChoicePanel;

    public PrizeChoicePanelManager(PrizeChoicePanel prizeChoicePanel)
    {
        _prizeChoicePanel = prizeChoicePanel;
    }

    public void Enable() => _prizeChoicePanel.IsVisible = true;
    public void Disable() => _prizeChoicePanel.IsVisible = false;
}
