using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.UnregisteredUsers;

public class UnregisteredUserCreateDto
{
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль незарегистрированного пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("password")]
    public string Password { get; set; }

    public UnregisteredUserCreateDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}