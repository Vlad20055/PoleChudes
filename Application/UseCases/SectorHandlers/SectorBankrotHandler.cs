using Application.Managers;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorBankrotHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlayerManager? _playerManager = null;
    private ISectorHandler.State _state = ISectorHandler.State.Completed_Change;

    public SectorBankrotHandler(PresenterManager presenterManager)
    {
        _presenterManager = presenterManager;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("SectorBankrot\nПереход хода.");
        await Task.Delay(1500);
        _presenterManager.SetMessage("Вращайте барабан.");
        if (_playerManager != null) _playerManager.RemoveScore();

        return _state;
    }
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;
}
