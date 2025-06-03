using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorKeyHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private KeyPanelManager _keyPanelManager;
    private KeyChoicePanelManager _keyChoicePanelManager;
    private PlayerManager? _playerManager = null;
    private ISectorHandler.State _state;
    private TaskCompletionSource<bool> _choiceTaskCompletionSource = new TaskCompletionSource<bool>();
    private TaskCompletionSource<char> _keyTaskCompletionSource = new TaskCompletionSource<char>();

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;
    public void OnChoiceSelected(bool want) => _choiceTaskCompletionSource.TrySetResult(want);
    public void OnKeySelected(char keyNumber) => _keyTaskCompletionSource.TrySetResult(keyNumber);

    public SectorKeyHandler(
        PresenterManager presenterManager,
        KeyPanelManager keyPanelManager,
        KeyChoicePanelManager keyChoicePanelManager)
    {
        _presenterManager = presenterManager;
        _keyPanelManager = keyPanelManager;
        _keyChoicePanelManager = keyChoicePanelManager;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("Сектор КЛЮЧ на барабане!\nХотите попробовать?");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        bool want = false;

        if (_playerManager is PlayerAIManager playerAIManager) // AI
        {
            await Task.Delay(1000);
            want = playerAIManager.WantTrySectorKey();
        }
        else // Player
        {
            _keyChoicePanelManager.Enable();
            _choiceTaskCompletionSource = new TaskCompletionSource<bool>();
            want = await _choiceTaskCompletionSource.Task;
            _keyChoicePanelManager.Disable();
        }

        if (!want) // говорим игре, что нужно поменять ISectorHandler на SectorScoreHandler
        {
            if (_playerManager != null) _playerManager.SetMessage("Буду играть!");
            await Task.Delay(1000);
            if (_playerManager != null) _playerManager.SetMessage(string.Empty);
            _state = ISectorHandler.State.Incompleted;
            return _state;
        }

        if (_playerManager != null) _playerManager.SetMessage("Ключ!");
        await Task.Delay(1000);
        if (_playerManager != null) _playerManager.SetMessage(string.Empty);

        char keyNumber = '*';
        _keyPanelManager.Enable();

        if (_playerManager is PlayerAIManager PlayerAIManager) // AI
        {
            await Task.Delay(1000);
            keyNumber = PlayerAIManager.SelectKey();
        }
        else // Player
        {
            _keyTaskCompletionSource = new TaskCompletionSource<char>();
            keyNumber = await _keyTaskCompletionSource.Task;
        }

        _keyPanelManager.SelectKey(keyNumber);
        await Task.Delay(1500);
        processSelectedKey();
        await Task.Delay(1500);
        _keyPanelManager.SetDefaultState();
        _keyPanelManager.Disable();
        _state = ISectorHandler.State.Completed_Change;

        return _state;
    }
    private void processSelectedKey()
    {
        Random rnd = new Random();
        int temp = rnd.Next(0, 100);
        if (temp <= 100 / _keyPanelManager.KeyPanel.KeyUnits.Count) processCorrectKey();
        else processIncorrectKey();
    }
    private void processCorrectKey()
    {
        _presenterManager.SetMessage("Угадал!\nАвтомобиль Ваш!");
    }
    private void processIncorrectKey()
    {
        _presenterManager.SetMessage("Увы.\nПовезёт в следующий раз.");
    }
}
