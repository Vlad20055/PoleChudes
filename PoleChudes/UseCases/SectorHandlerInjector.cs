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
        //_sectorKeyHandler.Score = 1000;
        //sectorHandler = _sectorKeyHandler;
        switch (sectorNumber)
        {
            case 0:
                sectorHandler = _sectorPlusHandler;
                break;
            case 1:
                _sectorScoreHandler.Score = 700;
                sectorHandler = _sectorScoreHandler;
                break;
            case 2:
                _sectorScoreHandler.Score = 800;
                sectorHandler = _sectorScoreHandler;
                break;
            case 3:
                sectorHandler = _sectorPrizeHandler;
                break;
            case 4:
                _sectorScoreHandler.Score = 500;
                sectorHandler = _sectorScoreHandler;
                break;
            case 5:
                _sectorKeyHandler.Score = 1000;
                sectorHandler = _sectorKeyHandler;
                break;
            case 6:
                sectorHandler = _sectorBankrotHandler;
                break;
            case 7:
                _sectorScoreHandler.Score = 1000;
                sectorHandler = _sectorScoreHandler;
                break;
            case 8:
                _sectorScoreHandler.Score = 600;
                sectorHandler = _sectorScoreHandler;
                break;
            default:
                throw new Exception("Cannot find current sector");
        }
    }
}
