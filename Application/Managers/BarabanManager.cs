using Domain.Entities;

namespace Application.Managers;

public class BarabanManager
{
    private Baraban _baraban { get; set; }

    public event Action? StartRotation;

    public BarabanManager(Baraban baraban)
    {
        _baraban = baraban;
    }
    public int EvaluateCurrentSector()
    {
        float tempAngle = _baraban.Angle + 60f;
        tempAngle %= 360;
        int tempSector = (int)Math.Floor(tempAngle / 40);
        return 8 - tempSector;
    }

    public void RotateBaraban() => StartRotation?.Invoke();
}
