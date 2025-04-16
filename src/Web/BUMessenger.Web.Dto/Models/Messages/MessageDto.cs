using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Messages;

public class MessageDto
{
    /// <summary>
    /// Идентификатор сообщения
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("chatId")]
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Идентификатор отправителя
    /// </summary>
    [JsonPropertyName("senderId")]
    public Guid? CreatorId { get; set; }
    
    /// <summary>
    /// Идентификатор родительского сообщения
    /// </summary>
    [JsonPropertyName("parentMessageId")]
    public Guid? ParentMessageId { get; set; }
    
    /// <summary>
    /// Время отправления
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("sentAtUtc")]
    public DateTime SentAtUtc { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("text")]
    public string MessageText { get; set; }
    
    public MessageDto(Guid id, 
        Guid chatId, 
        Guid? senderId,
        Guid? parentMessageId,
        DateTime sentAtUtc,
        string messageText)
    {
        Id = id;
        ChatId = chatId;
        CreatorId = senderId;
        ParentMessageId = parentMessageId;
        SentAtUtc = sentAtUtc;
        MessageText = messageText;
    }
}