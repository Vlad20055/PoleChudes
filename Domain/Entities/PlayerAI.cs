namespace Domain.Entities;

public class PlayerAI : Player
{
    public event Action<char>? KeySelected;
    //public event Action? LetterSelected;
    //public event Action? PrizeSelected;
    public PlayerAI(int id, bool isCurrentPlayer) : base(id, isCurrentPlayer) { }

    //public async override void SelectKey()
    //{
    //    await Task.Delay(1000);
    //    char[] keysNumbers = { '1', '2', '3'};
    //    Random random = new Random();
    //    KeySelected?.Invoke(keysNumbers[random.Next(3)]);
    //}

    //public override void SelectLetter()
    //{
        
    //}

    //public override void SelectPrize()
    //{
        
    //}
}
