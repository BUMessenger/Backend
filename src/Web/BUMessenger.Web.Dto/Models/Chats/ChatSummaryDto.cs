using System.Text.Json.Serialization;
using BUMessenger.Domain.Models.Models.Messages;
using BUMessenger.Web.Dto.Models.Messages;

namespace BUMessenger.Web.Dto.Models.Chats;

public class ChatSummaryDto
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название чата
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("chatName")]
    public string ChatName { get; set; }
    
    /// <summary>
    /// Последнее сообщение в чате
    /// </summary>
    [JsonPropertyName("lastMessage")]
    public MessageDto? LastMessage { get; set; }
    
    /// <summary>
    /// Количество непрочитанных сообщений в чате
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("unreadMessagesNumber")]
    public int UnreadMessagesNumber { get; set; }
    
    public ChatSummaryDto(Guid id, 
        string chatName, 
        MessageDto? lastMessage, 
        int unreadMessagesNumber)
    {
        Id = id;
        ChatName = chatName;
        LastMessage = lastMessage;
        UnreadMessagesNumber = unreadMessagesNumber;
    }
}