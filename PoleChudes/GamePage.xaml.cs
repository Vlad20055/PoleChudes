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
        Random rand = new Random();
        double targetAngle = _barabanViewModel.Angle + 360 * rand.Next(5, 9) + rand.Next(0, 360);

        uint duration = 10000; // время анимации в миллисекундах
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