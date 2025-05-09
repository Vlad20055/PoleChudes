using PoleChudes.ViewModels;

namespace PoleChudes;

public partial class GamePage : ContentPage
{
    private GameBuilder _gameBuilder = new GameBuilder();
    private Game _game;

    private PlayersViewModel _playersPanelViewModel;
    private QuestionViewModel _questionViewModel;
    private AnswerPanelViewModel _answerPanelViewModel;
    private LettersPanelViewModel _lettersPanelViewModel;
    private PresenterViewModel _presenterViewModel;

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

        // create dependences
        BarabanContainer.Content = _game.Baraban;
        PlayersPanel.BindingContext = _playersPanelViewModel;
        Question.BindingContext = _questionViewModel;
        AnswerUnits.BindingContext = _answerPanelViewModel;
        LettersUnits.BindingContext = _lettersPanelViewModel;
        PresenterBox.BindingContext = _presenterViewModel;
    }

    private void OnSpinClicked(object sender, EventArgs e)
    {
        _game.PlayStep();
    }

    private void Letter_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            char letter = button.Text[0];
            _game.ProcessChosenLetter(letter);
        }
    }
}