using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorKeyHandler : ISectorHandler
{
    private PresenterManager _presenterManager;

    public SectorKeyHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorKey");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
    }
}
