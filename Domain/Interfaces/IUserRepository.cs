using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> RegisterAsync(User user);
    // возвращает false, если login уже занят
    Task<User?> AuthenticateAsync(string login, string passwordHash);
    Task<User?> GetByIdAsync(int userId);
}
