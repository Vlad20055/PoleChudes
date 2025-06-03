using Application.Managers;
using Domain.Entities;

namespace Application.UseCases;

public class SuperGameHandler
{
    private BarabanManager _barabanManager;
    private PresenterManager _presenterManager;
    private PrizesSuperGamePanelManager _prizesSuperGamePanelManager;
    private GameTaskManager _gameTaskManager;
    private AnswerPanelManager _answererPanelManager;
    private SuperGameChoicePanelManager _superGameChoicePanelManager;
    private LettersPanelManager _lettersPanelManager;
    private WordInputPanelManager _wordInputPanelManager;
    private TimerPanelManager _timerPanelManager;
    private TaskCompletionSource _prizeTaskCompletionSource = new();
    private TaskCompletionSource<bool> _superGameTaskCompletionSource = new();
    private TaskCompletionSource<char> _letterTaskCompletionSource = new();
    private TaskCompletionSource<string> _wordTaskCompletionSource = new();

    public SuperGameHandler(
        BarabanManager barabanManager,
        PresenterManager presenterManager,
        PrizesSuperGamePanelManager prizesSuperGamePanelManager,
        GameTaskManager gameTaskManager,
        AnswerPanelManager answererPanelManager,
        SuperGameChoicePanelManager superGameChoicePanelManager,
        LettersPanelManager lettersPanelManager,
        WordInputPanelManager wordInputPanelManager,
        TimerPanelManager timerPanelManager)
    {
        _barabanManager = barabanManager;
        _presenterManager = presenterManager;
        _prizesSuperGamePanelManager = prizesSuperGamePanelManager;
        _gameTaskManager = gameTaskManager;
        _answererPanelManager = answererPanelManager;
        _superGameChoicePanelManager = superGameChoicePanelManager;
        _lettersPanelManager = lettersPanelManager;
        _wordInputPanelManager = wordInputPanelManager;
        _timerPanelManager = timerPanelManager;
    }

    public void OnConfirmed() => _prizeTaskCompletionSource.TrySetResult();
    public void OnChoiceSelected(bool want) => _superGameTaskCompletionSource.TrySetResult(want);
    public void OnLetterSelected(char letter) => _letterTaskCompletionSource.TrySetResult(letter);
    public void OnWordClaimed(string word) => _wordTaskCompletionSource.TrySetResult(word);

    public async Task Handle()
    {
        _barabanManager.RemoveBaraban();
        await Task.Delay(1000);
        _presenterManager.SetMessage("!!! Поздравляю, вы выиграли !!!");
        await Task.Delay(2000);
        _lettersPanelManager.Disable();
        _lettersPanelManager.SetDefaultState();
        _prizesSuperGamePanelManager.Enable();
        _presenterManager.SetMessage("Выберите призы.\nНераспределённые очки мы вам отдадим деньгами!");

        // waiting for Player's choice
        _prizeTaskCompletionSource = new TaskCompletionSource();
        await _prizeTaskCompletionSource.Task;
        
        // some logic to save chosen prizes
        // ...

        _prizesSuperGamePanelManager.Disable();

        // sujjest SuperGame
        _presenterManager.SetMessage("Я предлагаю вам сыграть в СУПЕР-ИГРУ и выиграть СУПЕР-ПРИЗ!");
        _superGameChoicePanelManager.Enable();
        _superGameTaskCompletionSource = new TaskCompletionSource<bool>();
        bool want = await _superGameTaskCompletionSource.Task;
        _superGameChoicePanelManager.Disable();

        if (!want)
        {
            // end the game
            return;
        }

        // SUPER-GAME
        GameTask supertask = _gameTaskManager.GetRandomSuperTask();
        _answererPanelManager.SetTask(supertask);
        _presenterManager.SetMessage("У вас есть возможность назвать любые 7 букв и мы вам откроем их все!");
        _lettersPanelManager.Enable();
        _lettersPanelManager.UnblockAllLetters();

        string chosenLetters = string.Empty;
        char letter = '*';

        for (int i = 0; i < 7; ++i)
        {
            _letterTaskCompletionSource = new();
            letter = await _letterTaskCompletionSource.Task;
            _lettersPanelManager.SetColor(letter, "DarkSlateGray");
            _lettersPanelManager.BlockLetter(letter);
            chosenLetters += letter;
        }

        _presenterManager.SetMessage("Откройте нам эти буквы, если они вообще хоть где-нибудь есть.");
        await Task.Delay(1500);
        
        foreach (char l in chosenLetters)
        {
            if (_answererPanelManager.OpenLetter(l) == 0) _lettersPanelManager.SetColor(l, "Red");
            else _lettersPanelManager.SetColor(l, "Green");
        }

        await Task.Delay(1000);
        _presenterManager.SetMessage("У вас минута на то, чтобы отгадать слово.");
        _wordInputPanelManager.HideRefuse();
        _wordInputPanelManager.Enable();

        var timerTask = _timerPanelManager.StartTimer();
        _wordTaskCompletionSource = new();
        //string word = await _wordTaskCompetionSource.Task;

        var completed = await Task.WhenAny(
                _wordTaskCompletionSource.Task,
                timerTask ?? throw new Exception("Null reference to timerTask"));

        _wordInputPanelManager.Disable();

        if (completed == _wordTaskCompletionSource.Task) // названо слово
        {
            string word = await _wordTaskCompletionSource.Task;
            _timerPanelManager.CancelTimer();
            if (word.ToUpper() == _gameTaskManager.GetAnswer())
            {
                _presenterManager.SetMessage("Да! Абсолютно точно!\nПоздравляю с победой!");
            }
            else
            {
                _presenterManager.SetMessage("К сожалению, нет. Вы проиграли!");
            }
            return;
            
        }

        _presenterManager.SetMessage("Увы, время вышло. Вы проиграли!");
        return;
    }


    
}
