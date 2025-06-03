using Domain.Entities;

namespace Application.Managers;

public class WordInputPanelManager
{
    private WordInputPanel _wordInputPanel;

    public WordInputPanelManager(WordInputPanel wordInputPanel)
    {
        _wordInputPanel = wordInputPanel;
    }

    public void Enable() => _wordInputPanel.IsVisible = true;
    public void Disable() => _wordInputPanel.IsVisible = false;
    public void HideRefuse() => _wordInputPanel.IsRefuseVisible = false;
    public void ShowRefuse() => _wordInputPanel.IsRefuseVisible = true;
}
