using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorScoreHandler : ISectorHandler
{
    private Presenter _presenter;

    public SectorScoreHandler(Presenter presenter)
    {
        _presenter = presenter;
    }

    public async void Handle()
    {
        _presenter.Message = "SectorScore";
        await Task.Delay(1500);
        _presenter.Message = string.Empty;
    }
}
