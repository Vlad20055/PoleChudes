using UI.ViewModels;

namespace UI;

public partial class GamePage : ContentPage
{
    private GameBuilder _gameBuilder = new GameBuilder();
    private Game _game;

    private PlayersViewModel _playersPanelViewModel;
    private QuestionViewModel _questionViewModel;
    private AnswerPanelViewModel _answerPanelViewModel;
    private LettersPanelViewModel _lettersPanelViewModel;
    private PresenterViewModel _presenterViewModel;
    private KeyChoicePanelViewModel _keyChoicePanelViewModel;
    private KeyPanelViewModel _keyPanelViewModel;
    private PlusPanelViewModel _plusPanelViewModel;
    private PrizeChoicePanelViewModel _prizeChoicePanelViewModel;
    private PrizePanelViewModel _prizePanelViewModel;
    private BarabanViewModel _barabanViewModel;

    public GamePage()
    {
        InitializeComponent();

        // create _game
        _game = _gameBuilder.Build();

        // create ViewModels
        _playersPanelViewModel = new PlayersViewModel(_game.Player1, _game.Player2, _game.Player);
        _questionViewModel = new QuestionViewModel(_game.GameTask);
        _answerPanelViewModel = new AnswerPanelViewModel(_game.AnswerPanel);
        _lettersPanelViewModel = new LettersPanelViewModel(_game.LettersPanel);
        _presenterViewModel = new PresenterViewModel(_game.Presenter);
        _keyChoicePanelViewModel = new KeyChoicePanelViewModel(_game.KeyChoicePanel);
        _keyPanelViewModel = new KeyPanelViewModel(_game.KeyPanel);
        _plusPanelViewModel = new PlusPanelViewModel(_game.PlusPanel);
        _prizeChoicePanelViewModel = new PrizeChoicePanelViewModel(_game.PrizeChoicePanel);
        _prizePanelViewModel = new PrizePanelViewModel(_game.PrizePanel);
        _barabanViewModel = new BarabanViewModel(_game.Baraban);

        // create dependences
        BarabanPanel.BarabanContainer.Content = _barabanViewModel;
        PlayersPanel.BindingContext = _playersPanelViewModel;
        QuestionPanel.BindingContext = _questionViewModel;
        AnswerUnits.BindingContext = _answerPanelViewModel;
        LettersPanel.BindingContext = _lettersPanelViewModel;
        PresenterBox.BindingContext = _presenterViewModel;
        KeyChoicePanel.BindingContext = _keyChoicePanelViewModel;
        KeyPanel.BindingContext = _keyPanelViewModel;
        PlusPanel.BindingContext = _plusPanelViewModel;
        PrizeChoicePanel.BindingContext = _prizeChoicePanelViewModel;
        PrizePanel.BindingContext = _prizePanelViewModel;

        // subscribe on needed events
        KeyChoicePanel.Chosen += _game.SectorHandlerInjector.SectorKeyHandler.OnChoiceSelected;
        KeyPanel.KeySelected += _game.SectorHandlerInjector.SectorKeyHandler.OnKeySelected;
        PlusPanel.PositionSelected += _game.SectorHandlerInjector.SectorPlusHandler.ProcessSelectedPosition;
        PrizeChoicePanel.Chosen += _game.SectorHandlerInjector.SectorPrizeHandler.OnChoiceSelected;
        PrizePanel.PrizeSelected += _game.SectorHandlerInjector.SectorPrizeHandler.ProcessPrizeSelected;
        PrizePanel.MoneySelected += _game.SectorHandlerInjector.SectorPrizeHandler.ProcessMoneySelected;
        LettersPanel.LetterSelected += _game.SectorHandlerInjector.SectorScoreHandler.ProcessChosenLetter;
        _game.BarabanManager.StartRotation += _barabanViewModel.RotateAsync;
        BarabanPanel.SpinClicked += _barabanViewModel.RotateAsync;
        _barabanViewModel.RotationCompleted += _game.PlayStep;
    }

}