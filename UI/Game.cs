using Domain.Entities;
using Domain.Interfaces;
using Application.Managers;
using Application.UseCases;

namespace UI;

public class Game
{
    private TaskCompletionSource _barabanTaskCompletionSource = new TaskCompletionSource();
    private TaskCompletionSource<string?> _wordTaskCompletionSource = new TaskCompletionSource<string?>();

    public required BarabanManager BarabanManager;
    public required Baraban Baraban;
    public required Player Player1;
    public required Player Player2;
    public required Player Player;
    public required PlayerManager PlayerManager;
    public required PlayerAIManager PlayerAIManager;
    public required GameTaskManager GameTaskManager;
    public required GameTask GameTask;
    public required AnswerPanelManager AnswerPanelManager;
    public required AnswerPanel AnswerPanel;
    public required LettersPanelManager LettersPanelManager;
    public required LettersPanel LettersPanel;
    public required PresenterManager PresenterManager;
    public required Presenter Presenter;
    public required SectorHandlerInjector SectorHandlerInjector;
    public required KeyChoicePanelManager KeyChoicePanelManager;
    public required KeyChoicePanel KeyChoicePanel;
    public required KeyPanelManager KeyPanelManager;
    public required KeyPanel KeyPanel;
    public required PlusPanelManager PlusPanelManager;
    public required PlusPanel PlusPanel;
    public required PrizeChoicePanelManager PrizeChoicePanelManager;
    public required PrizeChoicePanel PrizeChoicePanel;
    public required PrizePanelManager PrizePanelManager;
    public required PrizePanel PrizePanel;
    public required ISectorHandler sectorHandler;
    public required PrizesSuperGamePanelManager PrizesSuperGamePanelManager;
    public required PrizesSuperGamePanel PrizesSuperGamePanel;
    public required SuperGameHandler SuperGameHandler;
    public required SuperGameChoicePanel SuperGameChoicePanel;
    public required SuperGameChoicePanelManager SuperGameChoicePanelManager;
    public required WordInputPanel WordInputPanel;
    public required WordInputPanelManager WordInputPanelManager;
    public required TimerPanel TimerPanel;
    public required TimerPanelManager TimerPanelManager;
    public required MenuPanel MenuPanel;
    public required MenuPanelManager MenuPanelManager;

    public Player? CurrentPlayer;

    public void OnRotationCompleted() => _barabanTaskCompletionSource.TrySetResult();

    public async Task Play()
    {
        PlayerManager manager;

        while (true)
        {
            if (IsGameOver())
            {
                if (CurrentPlayer == Player)
                {
                    SuperGameHandler.SetScore(CurrentPlayer.Score);
                    await SuperGameHandler.Handle();
                }
                else
                {
                    if (CurrentPlayer != null) PresenterManager.SetMessage($"Поздравляю! {CurrentPlayer.Name} выиграл!");
                }
                return;
            }

            PresenterManager.SetMessage("Вращайте барабан.");

            if (CurrentPlayer != Player) // AI
            {
                manager = PlayerAIManager;
                await Task.Delay(2000);
                BarabanManager.RotateBaraban();
            }
            else // Player
            {
                manager = PlayerManager;
                MenuPanelManager.EnableAllButtons();
            }

            _barabanTaskCompletionSource = new TaskCompletionSource();
            _wordTaskCompletionSource = new TaskCompletionSource<string?>();

            var completed = await Task.WhenAny(
                _barabanTaskCompletionSource.Task,
                _wordTaskCompletionSource.Task);

            if (completed == _barabanTaskCompletionSource.Task) // rotate baraban completed
            {
                if (CurrentPlayer != null) manager.SetPlayer(CurrentPlayer);

                await PlayStepAsync(manager);
            }
            else // Player wants to claim the word
            {
                string? word = await _wordTaskCompletionSource.Task;
                if (word == null)
                {
                    continue;
                }
                else
                {
                    if (word.ToUpper() == GameTaskManager.GetAnswer())
                    {
                        PresenterManager.SetMessage("Да! Абсолютно точно!");
                        await Task.Delay(2000);
                        AnswerPanelManager.OpenAllAnswer();
                        continue;
                    }
                    else
                    {
                        PresenterManager.SetMessage("Нет. К сожалению, вы ошиблись.");
                        if (CurrentPlayer != null) CurrentPlayer.Active = false;
                        ChangePlayer();
                        continue;
                    }
                }
            }

            
        }
    }
    public async Task PlayStepAsync(PlayerManager manager)
    {
        SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, BarabanManager.EvaluateCurrentSector(), manager);
        ISectorHandler.State state = await sectorHandler.Handle();

        if (state == ISectorHandler.State.Incompleted)
        {
            SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, 7, manager);  // Score = 1000
            state = await sectorHandler.Handle();
        }

        if (state == ISectorHandler.State.Completed_Change) ChangePlayer();
    }
    public void ChangePlayer() // it is considered that at least one player is active
    {
        int currentPlayerId = 0;
        Player[] players = { Player, Player1, Player2 };

        for (int i = 0; i < 3; ++i)
        {
            if (players[i].CurrentPlayer)
            {
                currentPlayerId = i;
                break;
            }
        }
        
        players[currentPlayerId].CurrentPlayer = false;

        for (int i = currentPlayerId + 1; i < currentPlayerId + 4; ++i)
        {
            if (players[i % 3].Active)
            {
                players[i % 3].CurrentPlayer = true;
                CurrentPlayer = players[i % 3];
                break;
            }
        }
    }
    public bool IsGameOver()
    {
        if (Player.Active == false &&
            Player1.Active == false &&
            Player2.Active == false)
        {
            return false;
        }

        foreach (var el in AnswerPanel.AnswerUnits)
        {
            if (!el.IsOpened) return false;
        }

        return true;
    }

    public void OnWordButton()
    {
        MenuPanelManager.DisableAllButtons();
        WordInputPanelManager.Enable();
    }
    public void OnWordClaimed(string word)
    {
        WordInputPanelManager.Disable();
        _wordTaskCompletionSource.TrySetResult(word);
    }
    public void OnWordRefused()
    {
        WordInputPanelManager.Disable();
        _wordTaskCompletionSource.TrySetResult(null);
    }
}
