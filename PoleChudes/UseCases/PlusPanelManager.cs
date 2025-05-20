using PoleChudes.Domain.Entities;

namespace PoleChudes.UseCases;

public class PlusPanelManager
{

    public event Action<int>? PositionSelected;
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

    public void SelectPosition(int position)
    {
        PositionSelected?.Invoke(position);
    }


}
