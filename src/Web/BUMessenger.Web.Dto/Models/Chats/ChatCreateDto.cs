using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Chats;

public class ChatCreateDto
{
    /// <summary>
    /// Название чата
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("chatName")]
    public string ChatName { get; set; }

    /// <summary>
    /// Идентификаторы пользователей, которые будут добавлены в чат
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("usersIds")]
    public List<Guid> UsersIds { get; set; }

    public ChatCreateDto(string chatName, List<Guid> usersIds)
    {
        ChatName = chatName;
        UsersIds = usersIds;
    }
}