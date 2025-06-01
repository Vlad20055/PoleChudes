using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class Player : INotifyPropertyChanged
{
    private string _name = "Player";
    private int _score = 0;
    private string _message = string.Empty;
    private bool _currentPlayer = false;

    public int Id; // 0-(Player); 1-(Player1) 2-(Player2) (the same as index)
    public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    public int Score { get => _score; set { _score = value; OnPropertyChanged(); } }
    public int NumberOfRightClaimedLetters { get; set; } = 0;
    public bool Active { get; set; } = true;
    public bool CurrentPlayer { get => _currentPlayer; set { _currentPlayer = value; OnPropertyChanged(); } }
    public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

    public Player(int id, bool isCurrentPlayer)
    {
        Id = id;
        CurrentPlayer = isCurrentPlayer;
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
