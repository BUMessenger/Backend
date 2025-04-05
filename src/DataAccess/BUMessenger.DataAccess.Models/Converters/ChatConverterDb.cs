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

        var users = chatDb.ChatUserInfos?
            .Where(cui => cui.User != null)
            .Select(cui => cui.User!.ToDomain())
            .ToList() ?? [];

        return new Chat
        {
            Id = chatDb.Id,
            ChatName = chatDb.ChatName,
            Users = users
        };
    }
    
    [return: NotNullIfNotNull(nameof(chatDb))]
    public static ChatSummary? ToSummaryDomain(this ChatDb? chatDb, MessageDb? lastMessage, int unreadMessagesCount)
    {
        if (chatDb is null)
            return null;

        return new ChatSummary
        {
            Id = chatDb.Id,
            ChatName = chatDb.ChatName,
            LastMessage = lastMessage.ToDomain(),
            UnreadMessagesCount = unreadMessagesCount
        };
    }
}