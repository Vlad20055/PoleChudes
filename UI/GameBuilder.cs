using Domain.Entities;
using Application.UseCases;
using Application.UseCases.SectorHandlers;
using Application.Managers;

namespace UI;

public class GameBuilder
{
    public Game Build()
    {
        // create baraban for game
        Baraban baraban = new Baraban(); // serialized
        BarabanManager barabanManager = new BarabanManager(baraban);

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

        // create KeyPanel for game
        KeyPanelManager keyPanelManager = new KeyPanelManager();

        // create PlusPanel for game
        PlusPanelManager plusPanelManager = new PlusPanelManager();

        // create PrizePanel for game
        PrizePanelManager prizePanelManager = new PrizePanelManager();

        // create presenter for game
        Presenter presenter = new Presenter();
        PresenterManager presenterManager = new PresenterManager(presenter);

        // create SectorHandlers for game
        SectorBankrotHandler sectorBankrotHandler = new SectorBankrotHandler(presenterManager);
        SectorKeyHandler sectorKeyHandler = new SectorKeyHandler(presenterManager, keyPanelManager);
        SectorPlusHandler sectorPlusHandler = new SectorPlusHandler(presenterManager, plusPanelManager, answerPanelManager);
        SectorPrizeHandler sectorPrizeHandler = new SectorPrizeHandler(presenterManager, prizePanelManager);
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
            Baraban = baraban,
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
            KeyPanelManager = keyPanelManager,
            KeyPanel = keyPanelManager.KeyPanel,
            PlusPanelManager = plusPanelManager,
            PlusPanel = plusPanelManager.PlusPanel,
            PrizePanelManager = prizePanelManager,
            PrizePanel = prizePanelManager.PrizePanel,
        };

        configureCurrentPlayer(game);

        // subscribe game for needed events
        sectorScoreHandler.ScoreChange += game.UpdateScore;
        sectorScoreHandler.PlayerChange += game.ChangePlayer;
        sectorBankrotHandler.RemoveScore += game.RemoveScore;
        

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
