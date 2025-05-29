using Domain.Entities;

namespace Application.Managers;

public class PlusPanelManager
{
    public PlusPanel PlusPanel { get; set; } = new PlusPanel();

    public void Enable()
    {
        PlusPanel.IsVisible = true;
    }

    public void Disable()
    {
        PlusPanel.IsVisible = false;
    }

    public void SetAvailablePositions(List<int> positions)
    {
        PlusPanel.AvailablePositions = positions;
    }
}
