namespace BUMessenger.Domain.Models.Models.Users;

public record Users
{
    /// <summary>
    /// Количество элементов в выдаче без учета пагинации
    /// </summary>
    public required int Count { get; init; }

    /// <summary>
    /// Список пользователей
    /// </summary>
    public required List<User> Items { get; init; } 
}