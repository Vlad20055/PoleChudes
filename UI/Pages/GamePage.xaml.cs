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
    private KeyPanelViewModel _keyPanelViewModel;
    private PlusPanelViewModel _plusPanelViewModel;
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
        _keyPanelViewModel = new KeyPanelViewModel(_game.KeyPanel);
        _plusPanelViewModel = new PlusPanelViewModel(_game.PlusPanel);
        _prizePanelViewModel = new PrizePanelViewModel(_game.PrizePanel);
        _barabanViewModel = new BarabanViewModel(_game.Baraban);

        // create dependences
        BarabanPanel.BarabanContainer.Content = _barabanViewModel;
        PlayersPanel.BindingContext = _playersPanelViewModel;
        QuestionPanel.BindingContext = _questionViewModel;
        AnswerUnits.BindingContext = _answerPanelViewModel;
        LettersPanel.BindingContext = _lettersPanelViewModel;
        PresenterBox.BindingContext = _presenterViewModel;
        KeyPanel.BindingContext = _keyPanelViewModel;
        PlusPanel.BindingContext = _plusPanelViewModel;
        PrizePanel.BindingContext = _prizePanelViewModel;

        // subscribe on needed events
        KeyPanel.KeySelected += _game.KeyPanelManager.SelectKey;
        PlusPanel.PositionSelected += _game.PlusPanelManager.SelectPosition;
        PrizePanel.PrizeSelected += _game.PrizePanelManager.ProcessPrizeSelected;
        PrizePanel.MoneySelected += _game.PrizePanelManager.ProcessMoneySelected;
        LettersPanel.LetterSelected += _game.ProcessChosenLetter;
        BarabanPanel.SpinClicked += _barabanViewModel.RotateAsync;
        _barabanViewModel.RotationCompleted += _game.PlayStep;
    }

}