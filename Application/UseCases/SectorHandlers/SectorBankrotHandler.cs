using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorBankrotHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlayerManager? _playerManager = null;

    public event Action? SectorCompleted = null;
    public event Action? PlayerChange = null;

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    public SectorBankrotHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorBankrot\nПереход хода.");
        await Task.Delay(1500);
        _presenterManager.SetMessage("Вращайте барабан.");
        if (_playerManager != null) _playerManager.RemoveScore();
        PlayerChange?.Invoke();
        SectorCompleted?.Invoke();
    }
}
