using Domain.Entities;

namespace Application.UseCases;

public class BarabanManager
{
    private Baraban _baraban { get; set; }
    //private List<IImage?> _sectorImages = new();

    public BarabanManager(Baraban baraban)
    {
        _baraban = baraban;
    }
    public int EvaluateCurrentSector()
    {
        float tempAngle = _baraban.Angle + 60f;
        tempAngle %= 360;
        int tempSector = (int)Math.Floor(tempAngle / 40);
        return (8 - tempSector);
    }

}
