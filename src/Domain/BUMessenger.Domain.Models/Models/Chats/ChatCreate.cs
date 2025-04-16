namespace BUMessenger.Domain.Models.Models.Chats;

public record ChatCreate
{
    public required string ChatName { get; init; }
    public required List<Guid> UsersIds { get; init; }
}