using PoleChudes.Domain.Entities;

namespace PoleChudes.ViewModels;
public class PlayersViewModel
{
    public Player Player1 { get; } // AI Player
    public Player Player2 { get; } // AI Player
    public Player Player { get; }
    
    public PlayersViewModel(Player player1, Player player2, Player player)
    {
        Player1 = player1;
        Player2 = player2;
        Player = player;
    }
}

