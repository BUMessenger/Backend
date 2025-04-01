using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserPasswordUpdateDto
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("oldPassword")]
    public string OldPassword { get; set; }
    
    /// <summary>
    /// Новый пароль пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; }

    public UserPasswordUpdateDto(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}