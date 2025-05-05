using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;
using PoleChudes.UseCases;

namespace PoleChudes;

public class Game
{
    public required BarabanManager BarabanManager;
    public required Baraban Baraban;
    public required Player Player1;
    public required Player Player2;
    public required Player Player;
    public required GameTask GameTask;
    public required AnswerPanelManager AnswerPanelManager;
    public required AnswerPanel AnswerPanel;
    public required LettersPanelManager LettersPanelManager;
    public required LettersPanel LettersPanel;
    public required PresenterManager PresenterManager;
    public required Presenter Presenter;
    public required SectorHandlerInjector SectorHandlerInjector;
    public required ISectorHandler sectorHandler;

    public async void PlayStep()
    {
        await BarabanManager.RotateBaraban();
        SectorHandlerInjector.InjectSectorHandler(ref sectorHandler, BarabanManager.EvaluateCurrentSector());
        sectorHandler.Handle();
    }
}
