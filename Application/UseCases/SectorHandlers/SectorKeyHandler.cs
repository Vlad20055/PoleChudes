using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorKeyHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private KeyPanelManager _keyPanelManager;
    private KeyChoicePanelManager _keyChoicePanelManager;
    private PlayerManager? _playerManager = null;

    public event Action? SectorCompleted = null;
    public event Action? PlayerChange = null;
    public event Action? NeedToSetScoreHandler;

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    public SectorKeyHandler(
        PresenterManager presenterManager,
        KeyPanelManager keyPanelManager,
        KeyChoicePanelManager keyChoicePanelManager)
    {
        _presenterManager = presenterManager;
        _keyPanelManager = keyPanelManager;
        _keyChoicePanelManager = keyChoicePanelManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("Сектор КЛЮЧ на барабане!\nХотите попробовать?");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);

        if (_playerManager is PlayerAIManager playerAIManager)
        {
            await Task.Delay(1000);
            bool want = playerAIManager.WantTrySectorKey();
            OnChoiceSelected(want);
            return;
        }

        if (_playerManager is PlayerManager)
        {
            _keyChoicePanelManager.Enable();
            return;
        }
    }

    public async void OnChoiceSelected(bool want)
    {
        _keyChoicePanelManager.Disable();

        if (want)
        {
            _keyPanelManager.Enable();
            
            if (_playerManager is PlayerAIManager playerAIManager)
            {
                await Task.Delay(1000);
                char keyNumber = playerAIManager.SelectKey();
                processSelectedKey(keyNumber);
            }

            if (_playerManager is PlayerManager) return;
        }
        else // говорим игре, что нужно поменять ISectorHandler на SectorScoreHandler
        {
            NeedToSetScoreHandler?.Invoke();
        }
    }

    public void OnKeySelected(char keyNumber) => processSelectedKey(keyNumber);

    private void processSelectedKey(char keyNumber)
    {
        _keyPanelManager.SelectKey(keyNumber);
        Random rnd = new Random();
        int temp = rnd.Next(0, 100);
        if (temp <= 100 / _keyPanelManager.KeyPanel.KeyUnits.Count) processCorrectKey();
        else processIncorrectKey();
    }
    private async void processCorrectKey()
    {
        _presenterManager.SetMessage("Угадал!\nАвтомобиль Ваш!");
        await Task.Delay(1000);
        _keyPanelManager.SetDefaultState();
        _keyPanelManager.Disable();
        _presenterManager.SetMessage("Вращайте барабан.");
        PlayerChange?.Invoke();
        SectorCompleted?.Invoke();
    }
    private async void processIncorrectKey()
    {
        _presenterManager.SetMessage("Увы.\nПовезёт в следующий раз.");
        await Task.Delay(1000);
        _keyPanelManager.SetDefaultState();
        _keyPanelManager.Disable();
        _presenterManager.SetMessage("Вращайте барабан.");
        PlayerChange?.Invoke();
        SectorCompleted?.Invoke();
    }
}
