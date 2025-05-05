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
        Player player1 = new Player(); // serialized
        Player player2 = new Player(); // serialized
        Player player = new Player(); // serialized

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
        SectorBankrotHandler sectorBankrotHandler = new SectorBankrotHandler(presenter);
        SectorKeyHandler sectorKeyHandler = new SectorKeyHandler(presenter);
        SectorPlusHandler sectorPlusHandler = new SectorPlusHandler(presenter);
        SectorPrizeHandler sectorPrizeHandler = new SectorPrizeHandler(presenter);
        SectorScoreHandler sectorScoreHandler = new SectorScoreHandler(presenter);
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
            sectorHandler = sectorScoreHandler
        };

        return game;
    }
}
