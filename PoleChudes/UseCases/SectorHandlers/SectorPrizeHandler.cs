using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorPrizeHandler : ISectorHandler
{
    private Presenter _presenter;

    public SectorPrizeHandler(Presenter presenter)
    {
        _presenter = presenter;
    }
    public async void Handle()
    {
        _presenter.Message = "SectorPrize";
        await Task.Delay(1500);
        _presenter.Message = string.Empty;
    }
}
