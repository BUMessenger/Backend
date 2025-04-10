namespace BUMessenger.Domain.Models.Models;

public record PageFilters
{
    /// <summary>
    /// Количество пропускаемых записей
    /// </summary>
    public required int Skip { get; init; }
    
    /// <summary>
    /// Количество взятых записей
    /// </summary>
    public required int Limit { get; init; }
}