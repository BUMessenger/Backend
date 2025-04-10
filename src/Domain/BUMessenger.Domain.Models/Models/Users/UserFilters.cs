namespace BUMessenger.Domain.Models.Models.Users;

public record UserFilters
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string? Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public required string? Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public required string? Fathername { get; init; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public required string? Email { get; init; }
    
    /// <summary>
    /// Идентификатор чата пользователя
    /// </summary>
    public required Guid? ChatId { get; init; }
}