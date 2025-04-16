using System.Diagnostics.CodeAnalysis;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.Messages;

namespace BUMessenger.DataAccess.Models.Converters;

public static class MessageConverterDb
{
    [return: NotNullIfNotNull(nameof(messageDb))]
    public static Message? ToDomain(this MessageDb? messageDb)
    {
        if (messageDb is null)
            return null;
        
        return new Message
        {
            Id = messageDb.Id,
            ChatId = messageDb.ChatId,
            CreatorId = messageDb.CreatorId,
            ParentMessageId = messageDb.ParentMessageId,
            SentAtUtc = messageDb.SentAtUtc,
            MessageText = messageDb.MessageText
        };
    }
}