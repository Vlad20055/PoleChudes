using PoleChudes.Animation;
using PoleChudes.Domain.Entities;
using PoleChudes.ViewModels;

namespace PoleChudes;

public partial class GamePage : ContentPage
{
    private GameBuilder _gameBuilder = new GameBuilder();
    private Game _game;

    private BarabanViewModel _barabanViewModel;
    private PlayersPanelViewModel _playersPanelViewModel;

    public GamePage()
    {
        InitializeComponent();

        // create _game
        _game = _gameBuilder.Build();


        // create ViewModels
        _barabanViewModel = new BarabanViewModel();
        _playersPanelViewModel = new PlayersPanelViewModel(_game.Player1, _game.Player2, _game.Player);


        // create dependences
        // subscribing for angle changing
        _barabanViewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(BarabanViewModel.Angle))
            {
                _game.Baraban.BarabanSD.Angle = (float)_barabanViewModel.Angle;
                _game.Baraban.Invalidate();
            }
        };
        BarabanContainer.Content = _game.Baraban;
        // PlayersPanelViewModel
        PlayersPanel.BindingContext = _playersPanelViewModel;
    }

    private async void OnSpinClicked(object sender, EventArgs e)
    {
        Random rand = new Random();
        double targetAngle = _barabanViewModel.Angle + 360 * rand.Next(5, 9) + rand.Next(0, 360);

        uint duration = 10000; // time of animation in miliseconds
        double startAngle = _barabanViewModel.Angle;
        double delta = targetAngle - startAngle;

        await this.AnimateAsync(
            "Spin",
            (progress) =>
            {
                _barabanViewModel.Angle = startAngle + delta * progress;
            },
            16, duration, Easing.CubicOut
        );

        _barabanViewModel.Angle = targetAngle % 360;
    }
}