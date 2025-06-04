namespace Infrastructure;

public interface ISequenceGenerator
{
    /// <summary>
    /// Возвращает следующий int Id для коллекции с именем sequenceName.
    /// </summary>
    Task<int> GetNextIdAsync(string sequenceName);
}
