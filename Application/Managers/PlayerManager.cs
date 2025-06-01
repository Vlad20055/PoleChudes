using Domain.Entities;

namespace Application.Managers;

public class PlayerManager
{
    private Player? _player { get; set; } = null;

    public void SetPlayer(Player player) => _player = player;
    public void UpdateScore(int scoreChanged)
    {
        if (_player != null) _player.Score += scoreChanged;
    }
    public void RemoveScore()
    {
        if (_player != null) _player.Score = 0;
    }
    public void RemovePlayer()
    {
        if (_player != null) _player.Active = false;
    }
    public void SetMessage(string message)
    {
        if (_player != null) _player.Message = message;
    }
}
