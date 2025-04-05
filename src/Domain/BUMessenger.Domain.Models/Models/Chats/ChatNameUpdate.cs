namespace BUMessenger.Domain.Models.Models.Chats;

public record ChatNameUpdate
{
    public required string ChatName { get; init; }
}