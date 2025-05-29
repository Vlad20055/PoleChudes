using Domain.Entities;

namespace Application.Managers;

public class PrizePanelManager
{
    public PrizePanel PrizePanel = new PrizePanel();

    public void EnableButtons() => PrizePanel.AreButtonsVisible = true;
    public void DisableButtons() => PrizePanel.AreButtonsVisible = false;
    public void Enable() => PrizePanel.IsVisible = true;
    public void Disable() => PrizePanel.IsVisible = false;
    public void SetPrize(Prize prize)
    {
        PrizePanel.SelectedPrize = prize;
    }
    public void RemovePrize()
    {
        PrizePanel.SelectedPrize = null;
    }
}
