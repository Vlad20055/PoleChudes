using PoleChudes.ViewModels;

namespace PoleChudes;

public partial class GamePage : ContentPage
{
    private GameBuilder _gameBuilder = new GameBuilder();
    private Game _game;

    private PlayersViewModel _playersPanelViewModel;
    private QuestionViewModel _questionViewModel;

    public GamePage()
    {
        InitializeComponent();

        // create _game
        _game = _gameBuilder.Build();

        // create ViewModels
        _playersPanelViewModel = new PlayersViewModel(_game.Player1, _game.Player2, _game.Player);
        _questionViewModel = new QuestionViewModel(_game.GameTask);

        // create dependences
        BarabanContainer.Content = _game.Baraban;
        PlayersPanel.BindingContext = _playersPanelViewModel;
        Question.BindingContext = _questionViewModel;
    }

    private async void OnSpinClicked(object sender, EventArgs e)
    {
        await _game.barabanManager.RotateBaraban();
    }
}