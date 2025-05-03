using PoleChudes.ViewModels;

namespace PoleChudes;

public partial class GamePage : ContentPage
{
    private GameBuilder _gameBuilder = new GameBuilder();
    private Game _game;

    private PlayersViewModel _playersPanelViewModel;
    private QuestionViewModel _questionViewModel;
    private AnswerPanelViewModel _answerPanelViewModel;

    public GamePage()
    {
        InitializeComponent();

        // create _game
        _game = _gameBuilder.Build();

        // create ViewModels
        _playersPanelViewModel = new PlayersViewModel(_game.Player1, _game.Player2, _game.Player);
        _questionViewModel = new QuestionViewModel(_game.GameTask);
        _answerPanelViewModel = new AnswerPanelViewModel(_game.AnswerPanel);

        // create dependences
        BarabanContainer.Content = _game.Baraban;
        PlayersPanel.BindingContext = _playersPanelViewModel;
        Question.BindingContext = _questionViewModel;
        AnswerUnits.BindingContext = _answerPanelViewModel;
    }

    private async void OnSpinClicked(object sender, EventArgs e)
    {
        await _game.barabanManager.RotateBaraban();
    }
}