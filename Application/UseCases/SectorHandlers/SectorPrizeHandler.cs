using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorPrizeHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PrizePanelManager _prizePanelManager;
    private int currentMoneySujjestion = 700;
    private int numberOfMoneySujjestions = 0;
    private int maxNumberOfMoneySujjestions = 4;
    private List<Prize> prizes = new List<Prize>()
    {
        new Prize() {Id = 1, Name = "Микроволновая печь", PrizeImage = "microwave.jpg"},
        new Prize() {Id = 2, Name = "Стиральная машина", PrizeImage = "washing_machine.jpeg" },
        new Prize() {Id = 3, Name = "Мультиварка", PrizeImage = "multivarka.jpg" },
    };
    private Prize _moneyPrize = new Prize() { Id = 0, Name = "Деньги", PrizeImage = "money.jpg" };


    public SectorPrizeHandler(PresenterManager presenterManager, PrizePanelManager prizePanelManager)
    {
        _presenterManager = presenterManager;
        _prizePanelManager = prizePanelManager;

        _prizePanelManager.PrizeSelected += ProcessPrizeSelected;
        _prizePanelManager.MoneySelected += ProcessMoneySelected;
    }
    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPrize");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        numberOfMoneySujjestions = 0;
        _prizePanelManager.Enable();
        _prizePanelManager.EnableButtons();
        SujjestMoney();
        // waiting for player's choice
    }

    public async void ProcessPrizeSelected()
    {
        numberOfMoneySujjestions++;
        if (numberOfMoneySujjestions == maxNumberOfMoneySujjestions) // отдаём ему приз
        {
            _prizePanelManager.DisableButtons();
            RevealPrize();
            numberOfMoneySujjestions = 0;
            currentMoneySujjestion = 700;
            _presenterManager.SetMessage("Поздравляю, ПРИЗ ваш!");
            await Task.Delay(1500);
            _prizePanelManager.RemovePrize();
            _prizePanelManager.Disable();
            _presenterManager.SetMessage("Враащйте барабан!");
        }
        else // иначе предлагаем больше денег
        {
            SujjestMoney();
        }
    }
    public async void ProcessMoneySelected()
    {
        _prizePanelManager.DisableButtons();
        numberOfMoneySujjestions = 0;
        currentMoneySujjestion = 700;
        _prizePanelManager.SetPrize(_moneyPrize);
        _presenterManager.SetMessage("Забирайте ваши деньги.");
        await Task.Delay(1500);
        _prizePanelManager.RemovePrize();
        _prizePanelManager.Disable();
        _presenterManager.SetMessage("Враащйте барабан!");
    }

    public void RevealPrize()
    {
        if (prizes.Count == 0) return;

        var rand = new Random();
        var randomPrize = prizes[rand.Next(prizes.Count)];
        _prizePanelManager.SetPrize(randomPrize);
    }

    private void SujjestMoney()
    {
        Random random = new Random();
        int newSujjestion = random.Next((int)(currentMoneySujjestion * 1.2), (int)(currentMoneySujjestion * 1.4));
        currentMoneySujjestion = newSujjestion;
        _presenterManager.SetMessage($"Я предлагаю вам {currentMoneySujjestion} рублей и мы не открываем приз.");
        // waiting for players choice
    }
}
