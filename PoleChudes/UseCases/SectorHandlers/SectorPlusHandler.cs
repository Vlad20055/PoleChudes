using PoleChudes.Domain.Entities;
using PoleChudes.Domain.Interfaces;

namespace PoleChudes.UseCases.SectorHandlers;

public class SectorPlusHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PlusPanelManager _plusPanelManager;
    private AnswerPanelManager _answerPanelManager;

    public SectorPlusHandler(
        PresenterManager presenterManager,
        PlusPanelManager plusPanelManager,
        AnswerPanelManager answerPanelManager)
    {
        _presenterManager = presenterManager;
        _plusPanelManager = plusPanelManager;
        _answerPanelManager = answerPanelManager;
        _plusPanelManager.PositionSelected += ProcessSelectedPosition;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPlus");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        List<int> closedLetters = _answerPanelManager.GetClosedLetters();
        _plusPanelManager.Enable();
        _plusPanelManager.SetAvailablePositions(closedLetters);
        // waiting for letter selection
    }

    public async void ProcessSelectedPosition(int position)
    {
        _plusPanelManager.Disable();
        _answerPanelManager.OpenLetter(position);
        _presenterManager.SetMessage("Откройте!");
        await Task.Delay(1500);
        _presenterManager.SetMessage("Будете барабан вращать?");
    }
}
