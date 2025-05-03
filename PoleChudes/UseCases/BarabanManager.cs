using Microsoft.Maui.Graphics.Platform;
using PoleChudes.Domain.Entities;
using PoleChudes.Domain.ObjectsSD;
using PoleChudes.Animation;
using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace PoleChudes.UseCases;

public class BarabanManager
{
    public Baraban Baraban { get; set; }
    private List<IImage?> _sectorImages = new();

    public BarabanManager(BarabanSD barabanSD)
    {
        LoadSectorImages();
        Baraban = ConstructBaraban(barabanSD);
    }
    private Baraban ConstructBaraban(BarabanSD barabanSD)
    {
        Baraban baraban = new Baraban();
        baraban.SectorImages = _sectorImages;
        baraban.Angle = barabanSD.Angle;
        return baraban;
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
    public async Task RotateBaraban()
    {
        var rand = new Random();
        double start = Baraban.Angle;
        double target = start + 360 * rand.Next(5, 9) + rand.Next(0, 360);
        double delta = target - start;

        uint duration = 10_000; // ms

        
        await Baraban.AnimateAsync(
            "spin",
            p => Baraban.Angle = (float)(start + delta * p),
            rate: 16, length: duration, easing: Easing.CubicOut
        );

        
        Baraban.Angle = (float)(target % 360);
    }
}
