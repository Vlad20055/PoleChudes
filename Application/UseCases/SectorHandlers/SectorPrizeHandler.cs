using Application.Managers;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.SectorHandlers;

public class SectorPrizeHandler : ISectorHandler
{
    private PresenterManager _presenterManager;
    private PrizeChoicePanelManager _prizeChoicePanelManager;
    private PrizePanelManager _prizePanelManager;
    private PrizeList _prizeList;
    private PlayerManager? _playerManager = null;
    private int currentMoneySujjestion = 700;
    private int numberOfMoneySujjestions = 0;
    private int maxNumberOfMoneySujjestions = 4;
    private ISectorHandler.State _state;
    private TaskCompletionSource<bool> _choiceTaskCompletionSource = new TaskCompletionSource<bool>();
    private TaskCompletionSource<string> _prizeTaskCompletionSource = new TaskCompletionSource<string>();

    public SectorPrizeHandler(
        PresenterManager presenterManager,
        PrizePanelManager prizePanelManager,
        PrizeChoicePanelManager prizeChoicePanelManager,
        PrizeList prizeList)
    {
        _presenterManager = presenterManager;
        _prizePanelManager = prizePanelManager;
        _prizeChoicePanelManager = prizeChoicePanelManager;
        _prizeList = prizeList;
    }

    public async Task<ISectorHandler.State> Handle()
    {
        _presenterManager.SetMessage("Сектор ПРИЗ на барабане!\nЧто выбираете?");
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
            _choiceTaskCompletionSource = new TaskCompletionSource<bool>();
            want = await _choiceTaskCompletionSource.Task;
            _prizeChoicePanelManager.Disable();
        }

        if (!want) // говорим игре, что нужно поменять ISectorHandler на SectorScoreHandler
        {
            if (_playerManager != null) _playerManager.SetMessage("Буду играть!");
            await Task.Delay(1000);
            if (_playerManager != null) _playerManager.SetMessage(string.Empty);
            _state = ISectorHandler.State.Incompleted;
            return _state;
        }

        if (_playerManager != null) _playerManager.SetMessage("Приз!");
        await Task.Delay(1000);
        if (_playerManager != null) _playerManager.SetMessage(string.Empty);

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
                _prizeTaskCompletionSource = new TaskCompletionSource<string>();
                choice = await _prizeTaskCompletionSource.Task;
                if (choice == "money") break;
            }
        }
        
        _prizePanelManager.Enable();
        _prizePanelManager.DisableButtons();

        if (choice == "money") // отдаём деньги
        {
            _prizePanelManager.SetPrize(_prizeList.MoneyPrize);
            _presenterManager.SetMessage($"Забирайте ваши деньги.\nРовно {currentMoneySujjestion} руб.");
        }
        else // отдаём приз
        {
            _prizePanelManager.SetPrize(GetRandomPrize());
            _presenterManager.SetMessage("Поздравляю, ПРИЗ ваш!");
        }

        await Task.Delay(2000);
        numberOfMoneySujjestions = 0;
        currentMoneySujjestion = 700;
        _prizePanelManager.RemovePrize();
        _prizePanelManager.Disable();
        _prizePanelManager.DisableButtons();
        if (_playerManager != null) _playerManager.RemovePlayer(); // удаляем текущего игрока
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
        if (_prizeList.Prizes.Count == 0) throw new Exception("Can't find prizes in SectorPrizeHandler");

        var rand = new Random();
        var randomPrize = _prizeList.Prizes[rand.Next(_prizeList.Prizes.Count)];
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
