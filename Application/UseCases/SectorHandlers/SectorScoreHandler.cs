using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorScoreHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;
    private PlayerManager? _playerManager = null;
    private TaskCompletionSource<char> _taskCompletionSource = new TaskCompletionSource<char>();
    private string _answer;
    private string _rightLetters;
    private string _wrongLetters;
    private ISectorHandler.State _state;
    public int? Score { get; set; } = null;

    public SectorScoreHandler(
        PresenterManager presenterManager,
        string answer,
        AnswerPanelManager answerPanelManager,
        LettersPanelManager lettersPanelManager)
    {
        _presenterManager = presenterManager;
        _answer = answer;
        _answerPanelManager = answerPanelManager;
        _lettersPanelManager = lettersPanelManager;
        _rightLetters = string.Empty;
        _wrongLetters = string.Empty;

        ConfigureRightAndWrongStrings();
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("SectorScore");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        char choice = '*';

        if (_playerManager is PlayerAIManager playerAIManager) // AI
        {
            await Task.Delay(1000);
            choice = playerAIManager.SelectLetter(_rightLetters, _wrongLetters);
        }
        else // Player
        {
            _lettersPanelManager.UnblockPanelAccordingToColors();
            _taskCompletionSource = new TaskCompletionSource<char>();
            choice = await _taskCompletionSource.Task;
        }

        await ProcessChosenLetter(choice);
        _presenterManager.SetMessage("Вращайте барабан");
        return _state;
    }
    public void OnLetterSelected(char letter) => _taskCompletionSource.TrySetResult(letter);
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    private bool isCorrectLetter(char letter)
    {
        foreach (char el in _answer)
        {
            if (el == letter) return true;
        }
        return false;
    }
    private void RemoveRightLetter(char letter)
    {
        string temp = string.Empty;
        temp += letter;
        _rightLetters = _rightLetters.Replace(temp, string.Empty);
    }
    private void RemoveWrongLetter(char letter)
    {
        string temp = string.Empty;
        temp += letter;
        _wrongLetters = _wrongLetters.Replace(temp, string.Empty);
    }
    private async Task ProcessCorrectLetter(char letter)
    {
        RemoveRightLetter(letter);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1000);
        int numberOfOpenedLetters = _answerPanelManager.OpenLetter(letter);
        if (_playerManager != null) _playerManager.UpdateScore(Score * numberOfOpenedLetters ?? throw new Exception("Score is null"));
        _lettersPanelManager.SetColor(letter, "Green");
        _state = ISectorHandler.State.Completed_NoChange;
    }
    private async Task ProcessIncorrectLetter(char letter)
    {
        RemoveWrongLetter(letter);
        _presenterManager.SetMessage("Нет. Такой буквы нет.\nПереход хода");
        await Task.Delay(1000);
        _lettersPanelManager.SetColor(letter, "Red");
        _state = ISectorHandler.State.Completed_Change;
    }
    private void ConfigureRightAndWrongStrings()
    {
        Dictionary<char, bool> letters = new Dictionary<char, bool>();
        string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        foreach (char letter in alphabet)
        {
            letters[letter] = false;
        }
        foreach (char letter in _answer)
        {
            letters[letter] = true;
        }
        foreach (var letter in letters)
        {
            if (letter.Value) _rightLetters += letter.Key;
            else _wrongLetters += letter.Key;
        }
    }
    private async Task ProcessChosenLetter(char chosenLetter)
    {
        _lettersPanelManager.BlockPanel();
        if (isCorrectLetter(chosenLetter)) await ProcessCorrectLetter(chosenLetter);
        else await ProcessIncorrectLetter(chosenLetter);
    }
}
