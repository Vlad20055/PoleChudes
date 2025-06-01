using System.ComponentModel;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace UI.ViewModels;
public class PlayersViewModel : INotifyPropertyChanged
{
    private Player _player1 { get; } // AI Player
    public Player _player2 { get; } // AI Player
    public Player _player { get; }
    
    public PlayersViewModel(Player player1, Player player2, Player player)
    {
        _player1 = player1;
        _player2 = player2;
        _player = player;

        player1.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Player.Name))
                OnPropertyChanged(nameof(Name1));
            else if (e.PropertyName == nameof(Player.Score))
                OnPropertyChanged(nameof(Score1));
            else if (e.PropertyName == nameof(Player.Message))
                OnPropertyChanged(nameof(Message1));
            else if (e.PropertyName == nameof(Player.CurrentPlayer))
                OnPropertyChanged(nameof(CurrentPlayer1));
        };
        player2.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Player.Name))
                OnPropertyChanged(nameof(Name2));
            else if (e.PropertyName == nameof(Player.Score))
                OnPropertyChanged(nameof(Score2));
            else if (e.PropertyName == nameof(Player.Message))
                OnPropertyChanged(nameof(Message2));
            else if (e.PropertyName == nameof(Player.CurrentPlayer))
                OnPropertyChanged(nameof(CurrentPlayer2));
        };
        player.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Player.Name))
                OnPropertyChanged(nameof(Name));
            else if (e.PropertyName == nameof(Player.Score))
                OnPropertyChanged(nameof(Score));
            else if (e.PropertyName == nameof(Player.Message))
                OnPropertyChanged(nameof(Message));
            else if (e.PropertyName == nameof(Player.CurrentPlayer))
                OnPropertyChanged(nameof(CurrentPlayer));
        };
    }

    public string Name1 => _player1.Name;
    public string Name2 => _player2.Name;
    public string Name => _player.Name;
    public int Score1 => _player1.Score;
    public int Score2 => _player2.Score;
    public int Score => _player.Score;
    public string Message1 => _player1.Message;
    public string Message2 => _player2.Message;
    public string Message => _player.Message;
    public bool CurrentPlayer1 => _player1.CurrentPlayer;
    public bool CurrentPlayer2 => _player2.CurrentPlayer;
    public bool CurrentPlayer => _player.CurrentPlayer;


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName]string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

