using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserCreateDto
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// Код подтверждения
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("approveCode")]
    public string ApproveCode { get; set; }

    public UserCreateDto(string email, string approveCode)
    {
        Email = email;
        ApproveCode = approveCode;
    }
}