using Application.Managers;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorPrizeHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PrizeChoicePanelManager _prizeChoicePanelManager;
    private PrizePanelManager _prizePanelManager;
    private PlayerManager? _playerManager = null;
    public event Action? NeedToSetScoreHandler;
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

    public event Action? SectorCompleted = null;
    public event Action? PlayerChange = null;

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;


    public SectorPrizeHandler(
        PresenterManager presenterManager,
        PrizePanelManager prizePanelManager,
        PrizeChoicePanelManager prizeChoicePanelManager)
    {
        _presenterManager = presenterManager;
        _prizePanelManager = prizePanelManager;
        _prizeChoicePanelManager = prizeChoicePanelManager;
    }
    public async void Handle()
    {
        _presenterManager.SetMessage("SectorPrize");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        numberOfMoneySujjestions = 0;

        if (_playerManager is PlayerAIManager playerAIManager)
        {
            await Task.Delay(1000);
            bool want = playerAIManager.WantTrySectorPrize();
            return;
        }

        if (_playerManager is PlayerManager)
        {
            _prizeChoicePanelManager.Enable();
            return;
        }
    }

    public async void OnChoiceSelected(bool want)
    {
        _prizeChoicePanelManager.Disable();

        if (want)
        {
            

            if (_playerManager is PlayerAIManager playerAIManager)
            {
                await Task.Delay(1000);
                string choice = playerAIManager.SelectPrizeOrMoney();
                if (choice == "prize")
                {
                    ProcessPrizeSelected();
                }
                else if (choice == "money")
                {
                    ProcessMoneySelected();
                }
            }

            if (_playerManager is PlayerManager)
            {
                _prizePanelManager.Enable();
                _prizePanelManager.EnableButtons();
                SujjestMoney();
                return;
            }
        }
        else // говорим игре, что нужно поменять ISectorHandler на SectorScoreHandler
        {
            NeedToSetScoreHandler?.Invoke();
        }
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
            if (_playerManager != null) _playerManager.RemovePlayer(); // удаляем текущего игрока
            _presenterManager.SetMessage("Враащйте барабан!");
            PlayerChange?.Invoke();
            SectorCompleted?.Invoke();
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
        if (_playerManager != null) _playerManager.RemovePlayer(); // удаляем текущего игрока
        _presenterManager.SetMessage("Вращайте барабан!");
        PlayerChange?.Invoke();
        SectorCompleted?.Invoke();
    }

    public void RevealPrize()
    {
        if (prizes.Count == 0) return;

        var rand = new Random();
        var randomPrize = prizes[rand.Next(prizes.Count)];
        _prizePanelManager.SetPrize(randomPrize);
    }

    private async void SujjestMoney()
    {
        Random random = new Random();
        int newSujjestion = random.Next((int)(currentMoneySujjestion * 1.2), (int)(currentMoneySujjestion * 1.4));
        currentMoneySujjestion = newSujjestion;
        _presenterManager.SetMessage($"Я предлагаю вам {currentMoneySujjestion} рублей и мы не открываем приз.");

        

        if (_playerManager is PlayerAIManager playerAIManager)
        {
            await Task.Delay(1000);
            string choice = playerAIManager.SelectPrizeOrMoney();
            if (choice == "prize")
            {
                ProcessPrizeSelected();
            }
            else if (choice == "money")
            {
                ProcessMoneySelected();
            }
            return;
        }

        if (_playerManager is PlayerManager) return;
    }
}
