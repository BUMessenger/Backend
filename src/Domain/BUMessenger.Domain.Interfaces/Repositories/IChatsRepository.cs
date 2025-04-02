using BUMessenger.Domain.Models.Models.Chats;
using BUMessenger.Domain.Models.Models.Messages;

namespace BUMessenger.Domain.Interfaces.Repositories;

public interface IChatsRepository
{
    Task<Chat> CreateChatAsync(ChatCreate chatCreate);
    Task<List<Chat>> GetChatsForUserAsync(Guid userId, ChatsFilters filters);
    Task<Chat> GetChatByIdAsync(Guid chatId);
    Task UpdateChatNameAsync(Guid chatId, ChatNameUpdate chatNameUpdate);
    Task AddUserToChatAsync(Guid chatId, Guid userId);
    Task RemoveUserFromChatAsync(Guid chatId, Guid userId);
    Task<List<Message>> GetMessagesAsync(Guid chatId, MessagesFilters filters);
    Task<List<Message>> GetThreadMessagesAsync(Guid chatId, Guid parentMessageId, ThreadsFilters filters);
}