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
    public static string GenerateJwtToken(User user, 
        JwtSettings jwtSettings,
        Guid refreshTokenId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("RefreshTokenId", refreshTokenId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpiryMinutes),
            signingCredentials: credentials
        );

        return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}