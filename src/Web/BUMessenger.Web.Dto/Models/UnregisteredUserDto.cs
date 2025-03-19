using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models;

public class UnregisteredUserDto
{
    /// <summary>
    /// Идентификатор незарегистрированного пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    public UnregisteredUserDto(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}