using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;

namespace Infrastructure;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;
    private readonly ISequenceGenerator _seq;
    // это вспомогательный сервис для генерации целочисленных Id

    public MongoUserRepository(
        string connectionString,
        string dbName,
        ISequenceGenerator seqGenerator)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(dbName);

        _users = database.GetCollection<User>("users");
        _seq = seqGenerator;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        // проверяем уникальность Login
        var existing = await _users.Find(u => u.Login == user.Login).FirstOrDefaultAsync();
        if (existing != null) return false;

        // генерируем новый int Id через SequenceGenerator
        user.Id = await _seq.GetNextIdAsync("users");
        await _users.InsertOneAsync(user);
        return true;
    }

    public async Task<User?> AuthenticateAsync(string login, string passwordHash)
    {
        return await _users.Find(u => u.Login == login && u.PasswordHash == passwordHash)
                           .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }
}