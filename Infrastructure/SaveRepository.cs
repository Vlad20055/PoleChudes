using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Serialization;

namespace Infrastructure;

public class SaveRepository(SerializationService serializationService, IFileSystemPath fileSystemPath) : ISaveRepository
{
    private string PrepareFolder(string username)
    {
        string savedGameDir = Path.Combine(fileSystemPath.GetAppDataDirectory(), username);
        // Если папки нет — создаём
        if (!Directory.Exists(savedGameDir))
        {
            Directory.CreateDirectory(savedGameDir);
        }

        return savedGameDir;
    }

    public async Task SavePlayer(string username, Player player)
    {
        string savedGameDir = PrepareFolder(username);
        string filePath = Path.Combine(savedGameDir, "player.json");
        await serializationService.SerializeFileAsync(player, filePath, "json");
    }

    public async Task SavePlayer1(string username, Player player1)
    {
        string savedGameDir = PrepareFolder(username);
        string filePath = Path.Combine(savedGameDir, "player1.json");
        await serializationService.SerializeFileAsync(player1, filePath, "json");
    }

    public async Task SavePlayer2(string username, Player player1)
    {
        string savedGameDir = PrepareFolder(username);
        string filePath = Path.Combine(savedGameDir, "player2.json");
        await serializationService.SerializeFileAsync(player1, filePath, "json");
    }
}
