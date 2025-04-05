using BUMessenger.Domain.Models.Models.Messages;

namespace BUMessenger.Domain.Models.Models.Chats;

public record ChatSummary
{
    public required Guid Id { get; init; }
    public required string ChatName { get; init; }
    public required Message? LastMessage { get; init; }
    public required int UnreadMessagesCount { get; init; }
}