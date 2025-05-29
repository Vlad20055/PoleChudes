using Domain.Interfaces;
using Application.UseCases.SectorHandlers;
using Application.Managers;

namespace Application.UseCases;

public class SectorHandlerInjector
{
    public readonly SectorBankrotHandler SectorBankrotHandler;
    public readonly SectorKeyHandler SectorKeyHandler;
    public readonly SectorPlusHandler SectorPlusHandler;
    public readonly SectorPrizeHandler SectorPrizeHandler;
    public readonly SectorScoreHandler SectorScoreHandler;

    public SectorHandlerInjector(
        SectorBankrotHandler sectorBankrotHandler,
        SectorKeyHandler sectorKeyHandler,
        SectorPlusHandler sectorPlusHandler,
        SectorPrizeHandler sectorPrizeHandler,
        SectorScoreHandler sectorScoreHandler)
    {
        SectorBankrotHandler = sectorBankrotHandler;
        SectorKeyHandler = sectorKeyHandler;
        SectorPlusHandler = sectorPlusHandler;
        SectorPrizeHandler = sectorPrizeHandler;
        SectorScoreHandler = sectorScoreHandler;
    }

    public void InjectSectorHandler(ref ISectorHandler sectorHandler, int sectorNumber, PlayerManager playerManager)
    {
        switch (sectorNumber)
        {
            case 0:
                SectorPlusHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorPlusHandler;
                break;
            case 1:
                SectorScoreHandler.Score = 700;
                SectorScoreHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorScoreHandler;
                break;
            case 2:
                SectorScoreHandler.Score = 800;
                SectorScoreHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorScoreHandler;
                break;
            case 3:
                SectorPrizeHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorPrizeHandler;
                break;
            case 4:
                SectorScoreHandler.Score = 500;
                SectorScoreHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorScoreHandler;
                break;
            case 5:
                SectorKeyHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorKeyHandler;
                break;
            case 6:
                SectorBankrotHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorBankrotHandler;
                break;
            case 7:
                SectorScoreHandler.Score = 1000;
                SectorScoreHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorScoreHandler;
                break;
            case 8:
                SectorScoreHandler.Score = 600;
                SectorScoreHandler.SetPlayerManager(playerManager);
                sectorHandler = SectorScoreHandler;
                break;
            default:
                throw new Exception("Cannot find current sector");
        }
    }
}
