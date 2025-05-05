using PoleChudes.Domain.Interfaces;
using PoleChudes.UseCases.SectorHandlers;

namespace PoleChudes.UseCases;

public class SectorHandlerInjector
{
    private readonly SectorBankrotHandler _sectorBankrotHandler;
    private readonly SectorKeyHandler _sectorKeyHandler;
    private readonly SectorPlusHandler _sectorPlusHandler;
    private readonly SectorPrizeHandler _sectorPrizeHandler;
    private readonly SectorScoreHandler _sectorScoreHandler;

    public SectorHandlerInjector(
        SectorBankrotHandler sectorBankrotHandler,
        SectorKeyHandler sectorKeyHandler,
        SectorPlusHandler sectorPlusHandler,
        SectorPrizeHandler sectorPrizeHandler,
        SectorScoreHandler sectorScoreHandler)
    {
        _sectorBankrotHandler = sectorBankrotHandler;
        _sectorKeyHandler = sectorKeyHandler;
        _sectorPlusHandler = sectorPlusHandler;
        _sectorPrizeHandler = sectorPrizeHandler;
        _sectorScoreHandler = sectorScoreHandler;
    }

    public void InjectSectorHandler(ref ISectorHandler sectorHandler, int sectorNumber)
    {
        switch (sectorNumber)
        {
            case 0:
                sectorHandler = _sectorPlusHandler;
                break;
            case 3:
                sectorHandler = _sectorPrizeHandler;
                break;
            case 5:
                sectorHandler = _sectorKeyHandler;
                break;
            case 6:
                sectorHandler = _sectorBankrotHandler;
                break;
            default:
                sectorHandler = _sectorScoreHandler;
                break;
        }
    }
}
