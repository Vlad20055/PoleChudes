using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorScoreHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;
    private RightWrongLettersManager _rightWrongLettersManager;
    private PlayerManager? _playerManager = null;
    private TaskCompletionSource<char> _taskCompletionSource = new TaskCompletionSource<char>();
    private ISectorHandler.State _state;
    public int? Score { get; set; } = null;

    public SectorScoreHandler(
        PresenterManager presenterManager,
        AnswerPanelManager answerPanelManager,
        LettersPanelManager lettersPanelManager,
        RightWrongLettersManager rightWrongLettersManager)
    {
        _presenterManager = presenterManager;
        _answerPanelManager = answerPanelManager;
        _lettersPanelManager = lettersPanelManager;
        _rightWrongLettersManager = rightWrongLettersManager;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage($"{Score} очков. Буква...");
        await Task.Delay(1500);
        char choice = '*';

        if (_playerManager is PlayerAIManager playerAIManager) // AI
        {
            await Task.Delay(1000);
            choice = playerAIManager.SelectLetter(_rightWrongLettersManager.GetRightLetters(), _rightWrongLettersManager.GetWrongLetters());
        }
        else // Player
        {
            _lettersPanelManager.UnblockPanelAccordingToColors();
            _taskCompletionSource = new TaskCompletionSource<char>();
            choice = await _taskCompletionSource.Task;
        }

        if (_playerManager != null) _playerManager.SetMessage($"Буква {choice}");
        await ProcessChosenLetter(choice);
        if (_playerManager != null) _playerManager.SetMessage(string.Empty);
        _presenterManager.SetMessage("Вращайте барабан");
        return _state;
    }
    public void OnLetterSelected(char letter) => _taskCompletionSource.TrySetResult(letter);
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    private bool isCorrectLetter(char letter)
    {
        foreach (char el in _rightWrongLettersManager.GetAnswer())
        {
            if (el == letter) return true;
        }
        return false;
    }
    private async Task ProcessCorrectLetter(char letter)
    {
        _rightWrongLettersManager.RemoveRightLetter(letter);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1000);
        int numberOfOpenedLetters = _answerPanelManager.OpenLetter(letter);
        if (_playerManager != null) _playerManager.UpdateScore(Score * numberOfOpenedLetters ?? throw new Exception("Score is null"));
        _lettersPanelManager.SetColor(letter, "Green");
        _state = ISectorHandler.State.Completed_NoChange;
    }
    private async Task ProcessIncorrectLetter(char letter)
    {
        _rightWrongLettersManager.RemoveWrongLetter(letter);
        _presenterManager.SetMessage("Нет. Такой буквы нет.\nПереход хода");
        await Task.Delay(1000);
        _lettersPanelManager.SetColor(letter, "Red");
        _state = ISectorHandler.State.Completed_Change;
    }
    private async Task ProcessChosenLetter(char chosenLetter)
    {
        _lettersPanelManager.BlockPanel();
        if (isCorrectLetter(chosenLetter)) await ProcessCorrectLetter(chosenLetter);
        else await ProcessIncorrectLetter(chosenLetter);
    }
}
