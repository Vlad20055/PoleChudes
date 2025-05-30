using System.Diagnostics.Metrics;
using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorPlusHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlusPanelManager _plusPanelManager;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;
    private PlayerManager? _playerManager = null;
    private ISectorHandler.State _state = ISectorHandler.State.Completed_NoChange;
    private TaskCompletionSource<int> _taskCompletionSource = new TaskCompletionSource<int>();

    public SectorPlusHandler(
        PresenterManager presenterManager,
        PlusPanelManager plusPanelManager,
        AnswerPanelManager answerPanelManager,
        LettersPanelManager lettersPanelManager)
    {
        _presenterManager = presenterManager;
        _plusPanelManager = plusPanelManager;
        _answerPanelManager = answerPanelManager;
        _lettersPanelManager = lettersPanelManager;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("SectorPlus");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        List<int> closedLetters = _answerPanelManager.GetClosedLetters();
        int position = 0;

        if (_playerManager is PlayerAIManager playerAIManager) // AI
        {
            await Task.Delay(1000);
            position = playerAIManager.SelectPosition(closedLetters);
        }
        else // Player
        {
            _plusPanelManager.SetAvailablePositions(closedLetters);
            _plusPanelManager.Enable();
            _taskCompletionSource = new TaskCompletionSource<int>();
            position = await _taskCompletionSource.Task;
            _plusPanelManager.Disable();
        }

        _answerPanelManager.OpenLetter(position);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1500);
        _lettersPanelManager.SetColor(_answerPanelManager.GetLetter(position), "Green");
        _presenterManager.SetMessage("Будете барабан вращать?");

        return _state;
    }
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;
    public void OnPositionSelected(int position) => _taskCompletionSource.TrySetResult(position);
}
