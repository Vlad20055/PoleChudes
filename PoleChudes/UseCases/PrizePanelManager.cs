using PoleChudes.Domain.Entities;

namespace PoleChudes.UseCases;

public class PrizePanelManager
{
    public PrizePanel PrizePanel = new PrizePanel();
    public event Action? PrizeSelected;
    public event Action? MoneySelected;

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
    public void ProcessPrizeSelected()
    {
        PrizeSelected?.Invoke();
    }
    public void ProcessMoneySelected()
    {
        MoneySelected?.Invoke();
    }

}
