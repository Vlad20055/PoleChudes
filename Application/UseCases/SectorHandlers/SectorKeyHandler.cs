using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;


public class SectorKeyHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private KeyPanelManager _keyPanelManager;

    public int? Score { get; set; } = null;

    public SectorKeyHandler(
        PresenterManager presenterManager,
        KeyPanelManager keyPanelManager)
    {
        _presenterManager = presenterManager;
        _keyPanelManager = keyPanelManager;

        _keyPanelManager.KeySelected += processSelectedKey;
    }

    public async void Handle()
    {
        _presenterManager.SetMessage("Сектор КЛЮЧ на барабане!\nХотите попробовать?");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        _keyPanelManager.Enable();
        // waiting for key selection
    }

    private void processSelectedKey()
    {
        // here UI has already updated
        Random rnd = new Random();
        int temp = rnd.Next(0, 100);
        if (temp <= 100 / _keyPanelManager.KeyPanel.KeyUnits.Count) processCorrectKey();
        else processIncorrectKey();
    }
    private async void processCorrectKey()
    {
        _presenterManager.SetMessage("Угадал!\nАвтомобиль Ваш!");
        await Task.Delay(1000);
        _keyPanelManager.SetDefaultState();
        _keyPanelManager.Disable();
        _presenterManager.SetMessage("Вращайте барабан.");
    }
    private async void processIncorrectKey()
    {
        _presenterManager.SetMessage("Увы.\nПовезёт в следующий раз.");
        await Task.Delay(1000);
        _keyPanelManager.SetDefaultState();
        _keyPanelManager.Disable();
        _presenterManager.SetMessage("Вращайте барабан.");
    }
}
