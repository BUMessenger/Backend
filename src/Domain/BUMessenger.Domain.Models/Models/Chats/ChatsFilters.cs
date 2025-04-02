namespace BUMessenger.Domain.Models.Models.Chats;

public record ChatsFilters
{
    public required PageFilters PageFilters { get; init; }
}