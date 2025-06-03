using Domain.Entities;
using Application.Managers;
using Application.UseCases;
using Application.UseCases.SectorHandlers;

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
        PlayerManager playerManager = new PlayerManager();
        PlayerAIManager playerAIManager = new PlayerAIManager();

        // create task for game
        GameTaskManager gameTaskManager = new GameTaskManager();
        GameTask gameTask = gameTaskManager.GetRandomTask(); // serialized

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

        // create KeyChoicePanel for game
        KeyChoicePanel keyChoicePanel = new KeyChoicePanel();
        KeyChoicePanelManager keyChoicePanelManager = new KeyChoicePanelManager(keyChoicePanel);

        // create PrizeChoicePanel for game
        PrizeChoicePanel prizeChoicePanel = new PrizeChoicePanel();
        PrizeChoicePanelManager prizeChoicePanelManager = new PrizeChoicePanelManager(prizeChoicePanel);

        // create RightWrongLetters for game
        RightWrongLetters rightWrongLetters = new RightWrongLetters(gameTask.Answer);
        RightWrongLettersManager rightWrongLettersManager = new RightWrongLettersManager(rightWrongLetters);

        // create PrizeList for game
        PrizeList prizeList = new PrizeList();

        // create PrizesSuperGamePanel for game
        PrizesSuperGamePanel prizesSuperGamePanel = new PrizesSuperGamePanel(prizeList);
        PrizesSuperGamePanelManager prizesSuperGamePanelManager = new PrizesSuperGamePanelManager(prizesSuperGamePanel);

        // create SuperGameChoicePanel for game
        SuperGameChoicePanel superGameChoicePanel = new SuperGameChoicePanel();
        SuperGameChoicePanelManager superGameChoicePanelManager = new SuperGameChoicePanelManager(superGameChoicePanel);

        // create WordInputPanel for game
        WordInputPanel wordInputPanel = new WordInputPanel();
        WordInputPanelManager wordInputPanelManager = new WordInputPanelManager(wordInputPanel);

        // create Timer for supergame
        TimerPanel timerPanel = new TimerPanel();
        TimerPanelManager timerPanelManager = new TimerPanelManager(timerPanel);

        // create MenuPanel for game
        MenuPanel menuPanel = new MenuPanel();
        MenuPanelManager menuPanelManager = new MenuPanelManager(menuPanel);

        // create SectorHandlers for game
        SectorBankrotHandler sectorBankrotHandler = new SectorBankrotHandler(presenterManager);
        SectorKeyHandler sectorKeyHandler = new SectorKeyHandler(presenterManager, keyPanelManager, keyChoicePanelManager);
        SectorPlusHandler sectorPlusHandler = new SectorPlusHandler(presenterManager, plusPanelManager, answerPanelManager, lettersPanelManager, rightWrongLettersManager);
        SectorPrizeHandler sectorPrizeHandler = new SectorPrizeHandler(presenterManager, prizePanelManager, prizeChoicePanelManager, prizeList);
        SectorScoreHandler sectorScoreHandler = new SectorScoreHandler(presenterManager, answerPanelManager, lettersPanelManager, rightWrongLettersManager);
        SectorHandlerInjector sectorHandlerInjector = new SectorHandlerInjector(
            sectorBankrotHandler,
            sectorKeyHandler,
            sectorPlusHandler,
            sectorPrizeHandler,
            sectorScoreHandler);

        // create SuperGameHandler for game
        SuperGameHandler superGameHandler = new SuperGameHandler(
            barabanManager,
            presenterManager,
            prizesSuperGamePanelManager,
            gameTaskManager,
            answerPanelManager,
            superGameChoicePanelManager,
            lettersPanelManager,
            wordInputPanelManager,
            timerPanelManager
            );



        // create game with all their components
        Game game = new Game()
        {
            BarabanManager = barabanManager,
            Baraban = baraban,
            Player1 = player1,
            Player2 = player2,
            Player = player,
            PlayerManager = playerManager,
            PlayerAIManager = playerAIManager,
            GameTask = gameTask,
            GameTaskManager = gameTaskManager,
            AnswerPanelManager = answerPanelManager,
            AnswerPanel = answerPanelManager.AnswerPanel,
            LettersPanelManager = lettersPanelManager,
            LettersPanel = lettersPanelManager.LettersPanel,
            PresenterManager = presenterManager,
            Presenter = presenter,
            SectorHandlerInjector = sectorHandlerInjector,
            sectorHandler = sectorScoreHandler,
            KeyChoicePanelManager = keyChoicePanelManager,
            KeyChoicePanel = keyChoicePanel,
            KeyPanelManager = keyPanelManager,
            KeyPanel = keyPanelManager.KeyPanel,
            PlusPanelManager = plusPanelManager,
            PlusPanel = plusPanelManager.PlusPanel,
            PrizeChoicePanelManager = prizeChoicePanelManager,
            PrizeChoicePanel = prizeChoicePanel,
            PrizePanelManager = prizePanelManager,
            PrizePanel = prizePanelManager.PrizePanel,
            PrizesSuperGamePanelManager = prizesSuperGamePanelManager,
            PrizesSuperGamePanel = prizesSuperGamePanel,
            SuperGameHandler = superGameHandler,
            SuperGameChoicePanel = superGameChoicePanel,
            SuperGameChoicePanelManager = superGameChoicePanelManager,
            WordInputPanel = wordInputPanel,
            WordInputPanelManager = wordInputPanelManager,
            TimerPanel = timerPanel,
            TimerPanelManager = timerPanelManager,
            MenuPanel = menuPanel,
            MenuPanelManager = menuPanelManager,
        };

        configureCurrentPlayer(game);

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
