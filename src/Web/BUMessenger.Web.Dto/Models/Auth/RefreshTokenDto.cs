using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Auth;

public class RefreshTokenDto
{
    /// <summary>
    /// Рефреш токен
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; }

    public RefreshTokenDto(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}