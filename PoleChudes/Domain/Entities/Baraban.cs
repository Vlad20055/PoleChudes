using System.ComponentModel;
using System.Runtime.CompilerServices;
using IImage = Microsoft.Maui.Graphics.IImage;


namespace PoleChudes.Domain.Entities;

public class Baraban : GraphicsView, IDrawable
{
    private float _angle;
    public float Angle
    {
        get => _angle;
        set
        {
            if (_angle != value)
            {
                _angle = value;
                Invalidate();
            }
        }
    }
    public List<IImage?> SectorImages { set; get; } = new List<IImage?>();

    public Baraban()
    {
        Drawable = this;
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

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.Rotate(sa / 2);
        for (int i = 0; i < n; i++)
        {
            canvas.DrawLine(0, 0, r, 0);
            canvas.Rotate(sa);
        }
        canvas.Rotate(sa / 2);

        for (int i = 0; i < n; i++)
        {
            canvas.Rotate(sa);
            if (i < SectorImages.Count && SectorImages[i] is IImage img)
            {
                float w = 40, h = 40;
                float x = r * 0.8f - w / 2;
                float y = -h / 2;
                canvas.DrawImage(img, x, y, w, h);
            }
        }

        canvas.StrokeSize = 4;
        canvas.DrawCircle(0, 0, r);

        canvas.RestoreState();
    }
}
