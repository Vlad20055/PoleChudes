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
    private int currentMoneySujjestion = 700;
    private int numberOfMoneySujjestions = 0;
    private int maxNumberOfMoneySujjestions = 4;
    private ISectorHandler.State _state;
    private List<Prize> prizes = new List<Prize>()
    {
        new Prize() {Id = 1, Name = "Микроволновая печь", PrizeImage = "microwave.jpg"},
        new Prize() {Id = 2, Name = "Стиральная машина", PrizeImage = "washing_machine.jpeg" },
        new Prize() {Id = 3, Name = "Мультиварка", PrizeImage = "multivarka.jpg" },
    };
    private Prize _moneyPrize = new Prize() { Id = 0, Name = "Деньги", PrizeImage = "money.jpg" };
    private TaskCompletionSource<bool> _choiceTaskCompletionSource = new TaskCompletionSource<bool>();
    private TaskCompletionSource<string> _prizeTaskCompletionSource = new TaskCompletionSource<string>();

    public SectorPrizeHandler(
        PresenterManager presenterManager,
        PrizePanelManager prizePanelManager,
        PrizeChoicePanelManager prizeChoicePanelManager)
    {
        _presenterManager = presenterManager;
        _prizePanelManager = prizePanelManager;
        _prizeChoicePanelManager = prizeChoicePanelManager;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("SectorPrize");
        await Task.Delay(1500);
        _presenterManager.SetMessage(string.Empty);
        numberOfMoneySujjestions = 0;
        bool want = false;

        if (_playerManager is PlayerAIManager playerAIManager) // AI
        {
            await Task.Delay(1000);
            want = playerAIManager.WantTrySectorPrize();
        }
        else // Player
        {
            _prizeChoicePanelManager.Enable();
            want = await _choiceTaskCompletionSource.Task;
            _prizeChoicePanelManager.Disable();
        }

        if (!want) // говорим игре, что нужно поменять ISectorHandler на SectorScoreHandler
        {
            _state = ISectorHandler.State.Incompleted;
            return _state;
        }

        string choice = string.Empty;

        if (_playerManager is PlayerAIManager PlayerAIManager) // AI
        {
            while (CheckMoneySelection())
            {
                SujjestMoney();
                await Task.Delay(1000);
                choice = PlayerAIManager.SelectPrizeOrMoney();
                if (choice == "money") break;
            }
        }
        else // Player
        {
            _prizePanelManager.Enable();
            _prizePanelManager.EnableButtons();

            while (CheckMoneySelection())
            {
                SujjestMoney();
                choice = await _prizeTaskCompletionSource.Task;
                if (choice == "money") break;
            }
        }
        
        _prizePanelManager.Enable();
        _prizePanelManager.DisableButtons();

        if (choice == "money") // отдаём деньги
        {
            _prizePanelManager.SetPrize(_moneyPrize);
            _presenterManager.SetMessage($"Забирайте ваши деньги.\nРовно {currentMoneySujjestion} руб.");
        }
        else // отдаём приз
        {
            _prizePanelManager.SetPrize(GetRandomPrize());
            _presenterManager.SetMessage("Поздравляю, ПРИЗ ваш!");
        }

        numberOfMoneySujjestions = 0;
        currentMoneySujjestion = 700;
        _prizePanelManager.RemovePrize();
        _prizePanelManager.Disable();
        _prizePanelManager.DisableButtons();
        if (_playerManager != null) _playerManager.RemovePlayer(); // удаляем текущего игрока
        _presenterManager.SetMessage("Враащйте барабан!");
        _state = ISectorHandler.State.Completed_Change;

        return _state;
    }
    public void OnChoiceSelected(bool want) => _choiceTaskCompletionSource.TrySetResult(want);
    public void OnPrizeSelected(string prize) => _prizeTaskCompletionSource.TrySetResult(prize);
    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    private bool CheckMoneySelection()
    {
        numberOfMoneySujjestions++;
        return (numberOfMoneySujjestions == maxNumberOfMoneySujjestions ? false : true);
    }
    private Prize GetRandomPrize()
    {
        if (prizes.Count == 0) throw new Exception("Can't find prizes in SectorPrizeHandler");

        var rand = new Random();
        var randomPrize = prizes[rand.Next(prizes.Count)];
        return randomPrize;
    }
    private void SujjestMoney()
    {
        Random random = new Random();
        int newSujjestion = random.Next((int)(currentMoneySujjestion * 1.2), (int)(currentMoneySujjestion * 1.4));
        currentMoneySujjestion = newSujjestion;
        _presenterManager.SetMessage($"Я предлагаю вам {currentMoneySujjestion} руб и мы не открываем приз.");
    }

}
