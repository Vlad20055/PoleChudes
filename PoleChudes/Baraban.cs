using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;

namespace PoleChudes;

public class Baraban : GraphicsView, IDrawable
{
    public float Angle { get; set; } = 0;

    private readonly List<IImage?> _sectorImages = new();

    public Baraban()
    {
        Drawable = this;
        LoadSectorImages();
    }

    private void LoadSectorImages()
    {
        IImage? image;
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.sek1.png"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
        }

        _sectorImages.Add(image);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;
        float radius = Math.Min(centerX, centerY) - 10;

        canvas.Translate(centerX, centerY);
        canvas.Rotate(Angle);

        int sectorCount = 9;
        float sectorAngle = 360f / sectorCount;

        for (int i = 0; i < sectorCount; i++)
        {
            canvas.SaveState();
            canvas.Rotate(sectorAngle * i);

            if (i < _sectorImages.Count && _sectorImages[i] is IImage img)
            {
                float imageWidth = 40;
                float imageHeight = 40;
                float imageX = radius * 0.6f - imageWidth / 2;
                float imageY = -imageHeight / 2;

                canvas.DrawImage(img, imageX, imageY, imageWidth, imageHeight);
            }

            canvas.RestoreState();
        }

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 4;
        canvas.DrawCircle(0, 0, radius);

        canvas.RestoreState();
    }
}
