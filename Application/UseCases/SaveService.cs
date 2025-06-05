using Application.Interfaces;
using Domain.Entities;
using System.Text;
namespace Application.UseCases;

public class SaveService(ISaveRepository saveRepository)
{
    public async Task SaveGame(
        string username,
        Player player,
        Player player1,
        Player player2
        )
    {
        await saveRepository.SavePlayer(username, player);
        await saveRepository.SavePlayer1(username, player1);
        await saveRepository.SavePlayer2(username, player2);
    }
}
