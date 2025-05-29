using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorScoreHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;
    private PlayerManager? _playerManager = null;
    private string _answer;
    private string _rightLetters;
    private string _wrongLetters;

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    public int? Score { get; set; } = null;
    public event Action? PlayerChange = null;
    public event Action? SectorCompleted = null;

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

    private async void ProcessCorrectLetter(char letter)
    {
        RemoveRightLetter(letter);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1000);
        int numberOfOpenedLetters = _answerPanelManager.OpenLetter(letter);
        if (_playerManager != null) _playerManager.UpdateScore(Score * numberOfOpenedLetters ?? throw new Exception("Score is null"));
        _lettersPanelManager.SetColor(letter, "Green");
        await Task.Delay(1000);
        _presenterManager.SetMessage("Вращайте барабан");
        SectorCompleted?.Invoke();
    }

    private async void ProcessIncorrectLetter(char letter)
    {
        RemoveWrongLetter(letter);
        _presenterManager.SetMessage("Нет. Такой буквы нет.\nПереход хода");
        await Task.Delay(1000);
        _lettersPanelManager.SetColor(letter, "Red");
        _presenterManager.SetMessage("Вращайте барабан");
        PlayerChange?.Invoke();
        SectorCompleted?.Invoke();
    }

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

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorScore");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);

        if (_playerManager is PlayerAIManager playerAIManager)
        {
            await Task.Delay(1000);
            char choice = playerAIManager.SelectLetter(_rightLetters, _wrongLetters);
            ProcessChosenLetter(choice);
            return;
        }

        if (_playerManager is PlayerManager)
        {
            _lettersPanelManager.UnblockPanelAccordingToColors();
            return;
        }
    }

    public void ProcessChosenLetter(char chosenLetter)
    {
        _lettersPanelManager.BlockPanel();
        if (isCorrectLetter(chosenLetter)) ProcessCorrectLetter(chosenLetter);
        else ProcessIncorrectLetter(chosenLetter);
    }
}
