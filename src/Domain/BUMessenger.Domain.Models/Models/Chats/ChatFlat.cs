using BUMessenger.Domain.Models.Models.Messages;

namespace BUMessenger.Domain.Models.Models.Chats;

public record ChatFlat
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Message LastMessage { get; init; }
    public required int UnreadMessagesCount { get; init; }
}