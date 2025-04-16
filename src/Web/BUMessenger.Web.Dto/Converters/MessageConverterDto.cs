using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Messages;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.Messages;

namespace BUMessenger.Web.Dto.Converters;

public static class MessageConverterDto
{
    [return: NotNullIfNotNull(nameof(messageDto))]
    public static Message? ToDomain(this MessageDto? messageDto)
    {
        if (messageDto is null)
            return null;

        return new Message
        {
            Id = messageDto.Id,
            ChatId = messageDto.ChatId,
            CreatorId = messageDto.CreatorId,
            ParentMessageId = messageDto.ParentMessageId,
            SentAtUtc = messageDto.SentAtUtc,
            MessageText = messageDto.MessageText,
        };
    }
    
    [return: NotNullIfNotNull(nameof(message))]
    public static MessageDto? ToDto(this Message? message)
    {
        if (message is null)
            return null;

        return new MessageDto(id: message.Id,
            chatId: message.ChatId,
            senderId: message.CreatorId,
            parentMessageId: message.ParentMessageId,
            sentAtUtc: message.SentAtUtc,
            messageText: message.MessageText);
    }
    
    [return: NotNullIfNotNull(nameof(messages))]
    public static PagedDto<MessageDto>? ToDto(this Paged<Message>? messages)
    {
        if (messages is null)
            return null;

        return new PagedDto<MessageDto>(count: messages.Count,
            items: messages.Items.ConvertAll(ToDto)!);
    }
    
}