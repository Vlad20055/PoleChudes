using Domain.Entities;
using Domain.Interfaces;
using Application.Managers;
using Application.UseCases;

namespace UI;

public class Game
{
    private TaskCompletionSource _barabanTaskCompletionSource = new TaskCompletionSource();

    public required BarabanManager BarabanManager;
    public required Baraban Baraban;
    public required Player Player1;
    public required Player Player2;
    public required Player Player;
    public required PlayerManager PlayerManager;
    public required PlayerAIManager PlayerAIManager;
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

    public Player? CurrentPlayer;

    public void OnRotationCompleted() => _barabanTaskCompletionSource.TrySetResult();

    public async Task Play()
    {
        PlayerManager manager;

        while (true)
        {
            if (CurrentPlayer != Player) // AI
            {
                manager = PlayerAIManager;
                await Task.Delay(2000);
                BarabanManager.RotateBaraban();
            }
            else // Player
            {
                manager = PlayerManager;
            }

            _barabanTaskCompletionSource = new TaskCompletionSource();
            await _barabanTaskCompletionSource.Task;

            if (CurrentPlayer != null) manager.SetPlayer(CurrentPlayer);

            await PlayStepAsync(manager);
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
    public void ChangePlayer() // it is considered that it is possible to change current player correctly
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
}
