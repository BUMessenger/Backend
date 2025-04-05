namespace BUMessenger.Domain.Models.Models;

public record Paged<T>
{
    /// <summary>
    /// Общее количество элементов, игнорируя пагинацию.
    /// </summary>
    public required int Count { get; init; }

    /// <summary>
    /// Список элементов после пагинации.
    /// </summary>
    public required List<T> Items { get; init; } = new List<T>();
}