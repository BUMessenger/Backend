namespace BUMessenger.Domain.Models.Models.Users;

public record UserCreate
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Код подтверждения
    /// </summary>
    public required string ApproveCode { get; init; }
}