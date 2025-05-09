using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorBankrotHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    public SectorBankrotHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorBankrot");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
    }
}
