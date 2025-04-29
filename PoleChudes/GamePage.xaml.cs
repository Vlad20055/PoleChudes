using PoleChudes.Animation;

namespace PoleChudes;

public partial class GamePage : ContentPage
{
    private Game _game = new Game();
    private BarabanViewModel _barabanViewModel;

    public GamePage()
    {
        InitializeComponent();
        _barabanViewModel = new BarabanViewModel();
        // подписка на изменение угла
        _barabanViewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(BarabanViewModel.Angle))
            {
                _game.Baraban.Angle = (float)_barabanViewModel.Angle;
                _game.Baraban.Invalidate();
            }
        };
        BarabanContainer.Content = _game.Baraban;
    }

    private async void OnSpinClicked(object sender, EventArgs e)
    {
        // Анимация вращения барабана
        Random rand = new Random();
        double targetAngle = _barabanViewModel.Angle + 360 * rand.Next(3, 6) + rand.Next(0, 360);

        uint duration = 3000; // время анимации в миллисекундах

        await this.AnimateAsync(
            "Spin",
            (progress) =>
            {
                _barabanViewModel.Angle = _barabanViewModel.Angle + (targetAngle - _barabanViewModel.Angle) * progress;
            },
            16, duration, Easing.CubicOut
        );

        _barabanViewModel.Angle = targetAngle % 360;
    }
}