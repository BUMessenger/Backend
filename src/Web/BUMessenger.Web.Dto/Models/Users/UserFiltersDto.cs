using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserFiltersDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("surname")]
    public string? Surname { get; set; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("fathername")]
    public string? Fathername { get; set; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Идентификатор чата пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("chatId")]
    public Guid? ChatId { get; set; }

    public UserFiltersDto(string? name, 
        string? surname, 
        string? fathername, 
        string? email, 
        Guid? chatId)
    {
        Name = name;
        Surname = surname;
        Fathername = fathername;
        Email = email;
        ChatId = chatId;
    }
}