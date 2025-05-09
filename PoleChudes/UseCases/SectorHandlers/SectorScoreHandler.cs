using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorScoreHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private string _answer;
    private AnswerPanelManager _answerPanelManager;
    private LettersPanelManager _lettersPanelManager;

    public int? Score { get; set; } = null;
    public event Action<int>? ScoreChange = null;
    public event Action? PlayerChange = null;

    private bool isCorrectLetter(char letter)
    {
        foreach (char el in _answer)
        {
            if (el == letter) return true;
        }
        return false;
    }

    private async void ProcessCorrectLetter(char letter)
    {
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1000);
        int numberOfOpenedLetters = _answerPanelManager.OpenLetter(letter);
        ScoreChange?.Invoke(Score * numberOfOpenedLetters ?? throw new Exception("Score is null"));
        _lettersPanelManager.SetColor(letter, "Green");
        await Task.Delay(1000);
        _presenterManager.SetMessage("Вращайте барабан");
    }

    private async void ProcessIncorrectLetter(char letter)
    {
        _presenterManager.SetMessage("Нет. Такой буквы нет.\nПереход хода");
        await Task.Delay(1000);
        _lettersPanelManager.SetColor(letter, "Red");
        PlayerChange?.Invoke();
        _presenterManager.SetMessage("Вращайте барабан");
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
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorScore");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        _lettersPanelManager.UnblockPanelAccordingToColors();
        // waiting for click on letter in LettersPanel
    }

    public void ProcessChosenLetter(char chosenLetter)
    {
        _lettersPanelManager.BlockPanel();
        if (isCorrectLetter(chosenLetter)) ProcessCorrectLetter(chosenLetter);
        else ProcessIncorrectLetter(chosenLetter);
    }
}
