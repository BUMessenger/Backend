using System.Text.Json.Serialization;
using BUMessenger.Web.Dto.Models.Users;

namespace BUMessenger.Web.Dto.Models.Chats;

public class ChatDto
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
    /// Список пользователей в чате
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("users")]
    public List<UserDto> Users { get; set; }
    
    public ChatDto(Guid id, 
        string chatName, 
        List<UserDto> users)
    {
        Id = id;
        ChatName = chatName;
        Users = users;
    }
}