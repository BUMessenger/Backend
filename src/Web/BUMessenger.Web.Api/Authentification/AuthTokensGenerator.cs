using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BUMessenger.Domain.Models.Models.Users;
using BUMessenger.Web.Api.Authentification.Models;
using BUMessenger.Web.Dto.Models.Auth;
using Microsoft.IdentityModel.Tokens;

namespace BUMessenger.Web.Api.Authentification;

public static class AuthTokensGenerator
{
    public static AuthResponseDto GenerateTokensAsync(User user, JwtSettings jwtSettings)
    {
        var accessToken = GenerateJwtToken(user, jwtSettings);
        var refreshToken = GenerateRefreshToken();
        
        return new AuthResponseDto(accessToken, refreshToken);
    }
    
    private static string GenerateJwtToken(User user, JwtSettings jwtSettings)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}