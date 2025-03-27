using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Auth;

public class LoginUserDto
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("password")]
    public string Password { get; set; }

    public LoginUserDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}