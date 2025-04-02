using BUMessenger.Domain.Models.Models.Messages;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Models.Models.Chats;

public record Chat
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required List<User> Users { get; init; }
    public required List<Message> Messages { get; init; }
}