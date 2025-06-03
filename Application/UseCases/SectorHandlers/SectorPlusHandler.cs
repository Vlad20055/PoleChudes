using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorPlusHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlusPanelManager _plusPanelManager;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;
    private RightWrongLettersManager _rightWrongLettersManager;
    private PlayerManager? _playerManager = null;
    private ISectorHandler.State _state = ISectorHandler.State.Completed_NoChange;
    private TaskCompletionSource<int> _taskCompletionSource = new TaskCompletionSource<int>();

    public SectorPlusHandler(
        PresenterManager presenterManager,
        PlusPanelManager plusPanelManager,
        AnswerPanelManager answerPanelManager,
        LettersPanelManager lettersPanelManager,
        RightWrongLettersManager rightWrongLettersManager)
    {
        _presenterManager = presenterManager;
        _plusPanelManager = plusPanelManager;
        _answerPanelManager = answerPanelManager;
        _lettersPanelManager = lettersPanelManager;
        _rightWrongLettersManager = rightWrongLettersManager;
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

        if (_playerManager != null) _playerManager.SetMessage($"{position}-ю букву");
        await Task.Delay(1000);
        if (_playerManager != null) _playerManager.SetMessage(string.Empty);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1000);
        _answerPanelManager.OpenLetter(position);
        await Task.Delay(1000);
        char letter = _answerPanelManager.GetLetter(position);
        _lettersPanelManager.SetColor(letter, "Green");
        _rightWrongLettersManager.RemoveRightLetter(letter);

        return _state;
    }
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;
    public void OnPositionSelected(int position) => _taskCompletionSource.TrySetResult(position);
}
