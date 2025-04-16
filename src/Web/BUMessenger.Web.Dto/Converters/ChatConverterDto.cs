using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Chats;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;
using BUMessenger.Domain.Models.Models.Users;
using BUMessenger.Web.Dto.Models;
using BUMessenger.Web.Dto.Models.Chats;
using BUMessenger.Web.Dto.Models.UnregisteredUsers;
using BUMessenger.Web.Dto.Models.Users;

namespace BUMessenger.Web.Dto.Converters;

public static class ChatConverterDto
{
    [return: NotNullIfNotNull(nameof(chatSummary))]
    public static ChatSummaryDto? ToDto(this ChatSummary? chatSummary)
    {
        if (chatSummary is null)
            return null;
        
        return new ChatSummaryDto(id: chatSummary.Id,
            chatName: chatSummary.ChatName,
            lastMessage: chatSummary.LastMessage.ToDto(),
            unreadMessagesNumber: chatSummary.UnreadMessagesNumber);
    }

    [return: NotNullIfNotNull(nameof(chat))]
    public static ChatDto? ToDto(this Chat? chat)
    {
        if (chat is null)
            return null;
        
        return new ChatDto(id: chat.Id,
            chatName: chat.ChatName,
            users: chat.Users.ConvertAll(UserConverterDto.ToDto)!);
    }
    
    [return: NotNullIfNotNull(nameof(chatCreate))]
    public static ChatCreate? ToDomain(this ChatCreateDto? chatCreate)
    {
        if (chatCreate is null)
            return null;

        return new ChatCreate
        {
            ChatName = chatCreate.ChatName,
            UsersIds = chatCreate.UsersIds
        };
    }
    
    [return: NotNullIfNotNull(nameof(chatNameUpdateDto))]
    public static ChatNameUpdate? ToDomain(this ChatNameUpdateDto? chatNameUpdateDto)
    {
        if (chatNameUpdateDto is null)
            return null;

        return new ChatNameUpdate
        {
            ChatName = chatNameUpdateDto.ChatName
        };
    }
    
    [return: NotNullIfNotNull(nameof(chats))]
    public static PagedDto<ChatSummaryDto>? ToDto(this Paged<ChatSummary>? chats)
    {
        if (chats is null)
            return null;

        return new PagedDto<ChatSummaryDto>(count: chats.Count,
            items: chats.Items.ConvertAll(ToDto)!);
    }
}