using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;
using PoleChudes.Domain.ObjectsSD;

namespace PoleChudes.Domain.Entities;

public class Baraban : GraphicsView, IDrawable
{
    public BarabanSD BarabanSD { get; set; } = new BarabanSD();
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

        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.plus.jpg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.700.jpeg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.800.jpeg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.chest.png"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.500.jpeg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.key.png"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.bankrot.png"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.1000.jpeg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
        using (Stream? stream = assembly.GetManifestResourceStream("PoleChudes.Resources.Images.600.jpeg"))
        {
            if (stream != null) image = PlatformImage.FromStream(stream);
            else image = null;
            _sectorImages.Add(image);
        }
    }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        float centerX = dirtyRect.Width / 2;
        float centerY = dirtyRect.Height / 2;
        float radius = Math.Min(centerX, centerY) - 10;

        canvas.Translate(centerX, centerY);
        canvas.Rotate(BarabanSD.Angle);

        int sectorCount = 9;
        float sectorAngle = 360f / sectorCount;

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.Rotate(sectorAngle / 2);
        for (int i = 0; i < sectorCount; ++i)
        {
            canvas.DrawLine(0, 0, radius, 0);
            canvas.Rotate(sectorAngle);
        }
        canvas.Rotate(sectorAngle / 2);

        for (int i = 0; i < sectorCount; i++)
        {
            canvas.Rotate(sectorAngle);

            if (i < _sectorImages.Count && _sectorImages[i] is IImage img)
            {
                float imageWidth = 40;
                float imageHeight = 40;
                float imageX = radius * 0.8f - imageWidth / 2;
                float imageY = -imageHeight / 2;

                canvas.DrawImage(img, imageX, imageY, imageWidth, imageHeight);
            }
        }

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 4;
        canvas.DrawCircle(0, 0, radius);

        canvas.RestoreState();
    }
}
