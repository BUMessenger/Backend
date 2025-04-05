namespace BUMessenger.Domain.Models.Models.Users;

public record UserNameUpdate
{
    /// <summary>
    /// Новое имя пользователя
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Новая фамилия пользователя
    /// </summary>
    public required string Surname { get; init; }
    
    /// <summary>
    /// Новое отчество пользователя
    /// </summary>
    public required string? Fathername { get; init; }
}