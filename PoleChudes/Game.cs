using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;
using PoleChudes.UseCases;
using PoleChudes.UseCases.SectorHandlers;

namespace PoleChudes;

public class Game
{
    public required BarabanManager BarabanManager;
    public required Baraban Baraban;
    public required Player Player1;
    public required Player Player2;
    public required Player Player;
    public required GameTask GameTask;
    public required AnswerPanelManager AnswerPanelManager;
    public required AnswerPanel AnswerPanel;
    public required LettersPanelManager LettersPanelManager;
    public required LettersPanel LettersPanel;
    public required PresenterManager PresenterManager;
    public required Presenter Presenter;
    public required SectorHandlerInjector SectorHandlerInjector;
    public required KeyPanelManager KeyPanelManager;
    public required KeyPanel KeyPanel;
    public required PlusPanelManager PlusPanelManager;
    public required PlusPanel PlusPanel;
    public required ISectorHandler sectorHandler;

    public Player? CurrentPlayer;


    public async void PlayStep()
    {
        await BarabanManager.RotateBaraban();
        SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, BarabanManager.EvaluateCurrentSector());
        sectorHandler.Handle();
    }
    public void ProcessChosenLetter(char letter)
    {
        if (sectorHandler is SectorScoreHandler handler) handler.ProcessChosenLetter(letter);
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
    public void UpdateScore(int scoreChanged)
    {
        if (CurrentPlayer != null) CurrentPlayer.Score += scoreChanged;
    }
    public void RemoveScore()
    {
        if (CurrentPlayer != null) CurrentPlayer.Score = 0;
    }
    
}
