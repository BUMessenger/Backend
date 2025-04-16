namespace BUMessenger.Domain.Models.Models.Messages;

public record Message
{
    public required Guid Id { get; init; }
    public required Guid ChatId { get; init; }
    public Guid? CreatorId { get; init; }
    public Guid? ParentMessageId { get; init; }
    public required DateTime SentAtUtc { get; init; }
    public required string MessageText { get; init; }
}