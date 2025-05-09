using PoleChudes.Domain.Entities;
using PoleChudes.Domain.ObjectsSD;
using PoleChudes.UseCases;
using PoleChudes.UseCases.SectorHandlers;

namespace PoleChudes;

public class GameBuilder
{
    public Game Build()
    {
        // create baraban for game
        BarabanSD barabanSD = new BarabanSD(); // serialized
        BarabanManager barabanManager = new BarabanManager(barabanSD);

        // create players for game
        Player player = new Player(0, true); // serialized
        Player player1 = new Player(1, false); // serialized
        Player player2 = new Player(2, false); // serialized

        // create task for game
        GameTask gameTask = GameTaskManager.GetRandomTask(); // serialized

        // create AnswerPanel for game
        AnswerPanelManager answerPanelManager = new AnswerPanelManager(gameTask);

        // create LettersPanel for game
        LettersPanelManager lettersPanelManager = new LettersPanelManager();

        // create presenter for game
        PresenterManager presenterManager = new PresenterManager();
        Presenter presenter = presenterManager.Presenter;

        // create SectorHandlers for game
        SectorBankrotHandler sectorBankrotHandler = new SectorBankrotHandler(presenterManager);
        SectorKeyHandler sectorKeyHandler = new SectorKeyHandler(presenterManager);
        SectorPlusHandler sectorPlusHandler = new SectorPlusHandler(presenterManager);
        SectorPrizeHandler sectorPrizeHandler = new SectorPrizeHandler(presenterManager);
        SectorScoreHandler sectorScoreHandler = new SectorScoreHandler(presenterManager, gameTask.Answer, answerPanelManager, lettersPanelManager);
        SectorHandlerInjector sectorHandlerInjector = new SectorHandlerInjector(
            sectorBankrotHandler,
            sectorKeyHandler,
            sectorPlusHandler,
            sectorPrizeHandler,
            sectorScoreHandler);

        // create game with all their components
        Game game = new Game()
        {
            BarabanManager = barabanManager,
            Baraban = barabanManager.Baraban,
            Player1 = player1,
            Player2 = player2,
            Player = player,
            GameTask = gameTask,
            AnswerPanelManager = answerPanelManager,
            AnswerPanel = answerPanelManager.AnswerPanel,
            LettersPanelManager = lettersPanelManager,
            LettersPanel = lettersPanelManager.LettersPanel,
            PresenterManager = presenterManager,
            Presenter = presenter,
            SectorHandlerInjector = sectorHandlerInjector,
            sectorHandler = sectorScoreHandler,
        };

        configureCurrentPlayer(game);

        // subscribe game for needed events
        sectorScoreHandler.ScoreChange += game.UpdateScore;
        sectorScoreHandler.PlayerChange += game.ChangePlayer;

        

        return game;
    }

    private void configureCurrentPlayer(Game game)
    {
        Player[] players = { game.Player, game.Player1, game.Player2 };
        for (int i = 0; i < 3; ++i)
        {
            if (players[i].CurrentPlayer)
            {
                game.CurrentPlayer = players[i];
                break;
            }
        }
        if (game.CurrentPlayer == null) throw new Exception("Cannot find current player");
    }
}
