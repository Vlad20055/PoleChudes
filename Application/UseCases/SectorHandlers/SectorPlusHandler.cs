using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorPlusHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlusPanelManager _plusPanelManager;
    private AnswerPanelManager _answerPanelManager;
    private PlayerManager? _playerManager = null;

    public event Action? SectorCompleted = null;

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    public SectorPlusHandler(
        PresenterManager presenterManager,
        PlusPanelManager plusPanelManager,
        AnswerPanelManager answerPanelManager)
    {
        _presenterManager = presenterManager;
        _plusPanelManager = plusPanelManager;
        _answerPanelManager = answerPanelManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPlus");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        List<int> closedLetters = _answerPanelManager.GetClosedLetters();

        if (_playerManager is PlayerAIManager playerAIManager)
        {
            await Task.Delay(1000);
            int position = playerAIManager.SelectPosition(closedLetters);
            ProcessSelectedPosition(position);
            return;
        }

        if (_playerManager is PlayerManager)
        {
            _plusPanelManager.SetAvailablePositions(closedLetters);
            _plusPanelManager.Enable();
            return;
        }
    }

    public async void ProcessSelectedPosition(int position)
    {
        _plusPanelManager.Disable();
        _answerPanelManager.OpenLetter(position);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1500);
        _presenterManager.SetMessage("Будете барабан вращать?");
        SectorCompleted?.Invoke();
    }
}
