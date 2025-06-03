using Domain.Entities;

namespace Application.Managers;

public class PrizesSuperGamePanelManager
{
    private PrizesSuperGamePanel _prizesSuperGamePanel;

    public PrizesSuperGamePanelManager(PrizesSuperGamePanel prizesSuperGamePanel)
    {
        _prizesSuperGamePanel = prizesSuperGamePanel;
    }

    public void Enable() => _prizesSuperGamePanel.IsVisible = true;
    public void Disable() => _prizesSuperGamePanel.IsVisible = false;
    public void SetScore(int score) => _prizesSuperGamePanel.Score = score;
    public void OnPrizeSelected(string name)
    {
        foreach (var el in _prizesSuperGamePanel.Units)
        {
            if (el.Prize.Name == name)
            {
                if (el.IsSelected)
                {
                    el.IsSelected = false;
                    _prizesSuperGamePanel.Score += el.Prize.Cost;
                    return;
                }
                else if (_prizesSuperGamePanel.Score >= el.Prize.Cost)
                {
                    el.IsSelected = true;
                    _prizesSuperGamePanel.Score -= el.Prize.Cost;
                    return;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
