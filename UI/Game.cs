using Domain.Entities;
using Domain.Interfaces;
using Application.Managers;
using Application.UseCases;

namespace UI;

public class Game
{
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

    public Player? CurrentPlayer;

    public void ContinueGame()
    {
        if (CurrentPlayer == Player) return;
        else
        {
            BarabanManager.RotateBaraban();
        }
    }

    public void PlayStep()
    {
        PlayerManager manager;
        if (CurrentPlayer == Player) manager = PlayerManager;
        else manager = PlayerAIManager;
        if (CurrentPlayer != null) manager.SetPlayer(CurrentPlayer);
        SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, BarabanManager.EvaluateCurrentSector(), manager);
        sectorHandler.Handle();
    }

    public void PlaySectorMaxScoreHandler()
    {
        PlayerManager manager;
        if (CurrentPlayer == Player) manager = PlayerManager;
        else manager = PlayerAIManager;
        if (CurrentPlayer != null) manager.SetPlayer(CurrentPlayer);
        SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, 7, manager); // Score = 1000
        sectorHandler.Handle();
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
