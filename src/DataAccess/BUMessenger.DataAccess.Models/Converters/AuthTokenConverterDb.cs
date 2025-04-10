using System.Diagnostics.CodeAnalysis;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.AuthTokens;

namespace BUMessenger.DataAccess.Models.Converters;

public static class AuthTokenConverterDb
{
    [return: NotNullIfNotNull(nameof(authTokenCreate))]
    public static AuthTokenDb? ToDb(this AuthTokenCreate? authTokenCreate)
    {
        if (authTokenCreate is null)
            return null;

        return new AuthTokenDb(id: Guid.NewGuid(),
            userId: authTokenCreate.UserId,
            refreshToken: authTokenCreate.RefreshToken,
            expiresAtUtc: authTokenCreate.ExpiresAtUtc);
    }

    [return: NotNullIfNotNull(nameof(authTokenDb))]
    public static AuthToken? ToDomain(this AuthTokenDb? authTokenDb)
    {
        if (authTokenDb is null)
            return null;

        return new AuthToken
        {
            Id = authTokenDb.Id,
            UserId = authTokenDb.UserId,
            RefreshToken = authTokenDb.RefreshToken,
            ExpiresAtUtc = authTokenDb.ExpiresAtUtc,
        };
    }
}