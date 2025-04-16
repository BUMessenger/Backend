using System.Diagnostics.CodeAnalysis;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.Chats;
using BUMessenger.Domain.Models.Models.Messages;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.DataAccess.Models.Converters;

public static class ChatConverterDb
{
    [return: NotNullIfNotNull(nameof(chatDb))]
    public static Chat? ToDomain(this ChatDb? chatDb)
    {
        if (chatDb is null)
            return null;
        
        return new Chat
        {
            Id = chatDb.Id,
            ChatName = chatDb.ChatName,
            Users = chatDb.Users.Select(u => u.ToDomain()).ToList()
        };
    }
    
    [return: NotNullIfNotNull(nameof(chatDb))]
    public static ChatSummary? ToDomain(this ChatSummaryDb? chatDb)
    {
        if (chatDb is null)
            return null;

        return new ChatSummary
        {
            Id = chatDb.Id,
            ChatName = chatDb.ChatName,
            LastMessage = chatDb.LastMessage?.ToDomain(),
            UnreadMessagesNumber = chatDb.UnreadMessagesNumber
        };
    }
}