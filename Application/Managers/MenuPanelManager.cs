using Domain.Entities;

namespace Application.Managers;

public class MenuPanelManager
{
    private MenuPanel _menuPanel;

    public MenuPanelManager(MenuPanel menuPanel)
    {
        _menuPanel = menuPanel;
    }

    public void EnableRotateButton() => _menuPanel.IsRotateButtonVisible = true;
    public void DisableRotateButton() => _menuPanel.IsRotateButtonVisible = false;
    public void EnableWordButton() => _menuPanel.IsWordButtonVisible = true;
    public void DisableWordButton() => _menuPanel.IsWordButtonVisible = false;
    public void EnableAllButtons()
    {
        EnableRotateButton();
        EnableWordButton();
    }
    public void DisableAllButtons()
    {
        DisableRotateButton();
        DisableWordButton();
    }

}
