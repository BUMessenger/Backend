namespace BUMessenger.Domain.Models.Models.Users;

public record User
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public required string Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public required string? Fathername { get; init; }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public required string Email { get; init; }
}