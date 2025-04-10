using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Auth;

public class AuthResponseDto
{
    /// <summary>
    /// Авторизационный токен
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
    
    /// <summary>
    /// Рефреш токен
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; }

    public AuthResponseDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}