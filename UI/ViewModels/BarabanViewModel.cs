using Domain.Entities;
using UI.Animation;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace UI.ViewModels;

public class BarabanViewModel : GraphicsView, IDrawable
{
    public event Action? RotationCompleted;

    private Baraban _model;

    float _angle;
    public float Angle
    {
        get => _angle;
        set
        {
            if (_angle != value)
            {
                _angle = value;
                Invalidate();    // перерисовать
            }
        }
    }
    private List<IImage?> _sectorImages { get; set; } = new();
    public async void RotateAsync()
    {
        double start = _model.Angle;
        var rand = new Random();
        double target = start + 360 * rand.Next(5, 9) + rand.Next(360);
        double delta = target - start;

        await this.AnimateAsync(
            "spin",
            p => {
                float newAngle = (float)(start + delta * p);
                Angle = newAngle;
                _model.Angle = newAngle;
            },
            rate: 16,
            length: 10_000,
            easing: Easing.CubicOut);

        float final = (float)(target % 360);
        Angle = final;
        _model.Angle = final;

        RotationCompleted?.Invoke();
    }
    private List<IImage?> LoadSectorImages()
    {
        var list = new List<IImage?>();
        Assembly asm = GetType().GetTypeInfo().Assembly;

        // повторяем для всех 9 ресурсов
        foreach (var res in new[]
        {
                "UI.Resources.Images.plus.jpg",
                "UI.Resources.Images.700.jpeg",
                "UI.Resources.Images.800.jpeg",
                "UI.Resources.Images.chest.png",
                "UI.Resources.Images.500.jpeg",
                "UI.Resources.Images.key.png",
                "UI.Resources.Images.bankrot.png",
                "UI.Resources.Images.1000.jpeg",
                "UI.Resources.Images.600.jpeg",
            })
        {
            using var stream = asm.GetManifestResourceStream(res);
            list.Add(stream != null
                ? PlatformImage.FromStream(stream)
                : null);
        }
        return list;
    }
    public BarabanViewModel(Baraban model)
    {
        Drawable = this;
        _model = model;
        _angle = model.Angle;
        _sectorImages = LoadSectorImages();
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        float cx = dirtyRect.Width / 2;
        float cy = dirtyRect.Height / 2;
        float r = Math.Min(cx, cy) - 10;

        canvas.Translate(cx, cy);
        canvas.Rotate(Angle);

        int n = 9;
        float sa = 360f / n;

        // нарисовать сектора
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.Rotate(sa / 2);
        for (int i = 0; i < n; i++)
        {
            canvas.DrawLine(0, 0, r, 0);
            canvas.Rotate(sa);
        }
        canvas.Rotate(sa / 2);

        // нарисовать картинки
        for (int i = 0; i < n; i++)
        {
            canvas.Rotate(sa);
            if (i < _sectorImages.Count && _sectorImages[i] is IImage img)
            {
                float w = 40, h = 40;
                float x = r * 0.8f - w / 2;
                float y = -h / 2;
                canvas.DrawImage(img, x, y, w, h);
            }
        }

        // обводка
        canvas.StrokeSize = 4;
        canvas.DrawCircle(0, 0, r);

        canvas.RestoreState();
    }
}
