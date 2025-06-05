using Domain.Entities;

namespace Application.Interfaces;

public interface ISaveRepository
{
    public Task SavePlayer(string username, Player player);
    public Task SavePlayer1(string username, Player player1);
    public Task SavePlayer2(string username, Player player2);
}
