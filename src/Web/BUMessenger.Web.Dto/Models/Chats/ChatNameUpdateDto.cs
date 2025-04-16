using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Chats;

public class ChatNameUpdateDto
{
    /// <summary>
    /// Название чата
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("chatName")]
    public string ChatName { get; set; }
    
    public ChatNameUpdateDto(string chatName)
    {
        ChatName = chatName;
    }
}