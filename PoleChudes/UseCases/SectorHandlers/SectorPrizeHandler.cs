using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorPrizeHandler : ISectorHandler
{
    private PresenterManager _presenterManager;

    public SectorPrizeHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }
    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPrize");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
    }
}
