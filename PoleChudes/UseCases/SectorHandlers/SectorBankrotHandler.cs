using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorBankrotHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    public event Action? RemoveScore = null;

    public SectorBankrotHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorBankrot\nПереход хода.");
        await Task.Delay(1500);
        _presenterManager.SetMessage("Вращайте барабан.");
        RemoveScore?.Invoke();
    }
}
