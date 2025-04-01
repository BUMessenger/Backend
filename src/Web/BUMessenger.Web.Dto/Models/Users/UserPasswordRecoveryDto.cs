using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserPasswordRecoveryDto
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    public UserPasswordRecoveryDto(string email)
    {
        Email = email;
    }
}