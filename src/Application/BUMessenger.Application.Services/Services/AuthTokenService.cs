using BUMeesenger.Domain.Exceptions.Repositories.AuthTokenExceptions;
using BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.AuthTokens;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly IAuthTokenRepository _authTokenRepository;
    private readonly ILogger<IAuthTokenService> _logger;

    public AuthTokenService(IAuthTokenRepository authTokenRepository, ILogger<IAuthTokenService> logger)
    {
        _authTokenRepository = authTokenRepository ?? throw new ArgumentNullException(nameof(authTokenRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AuthToken> AddAuthTokenAsync(AuthTokenCreate authTokenCreate)
    {
        try
        {
            return await _authTokenRepository.AddAuthTokenAsync(authTokenCreate);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to add auth token {AuthToken}.", authTokenCreate);
            throw new AuthTokenServiceException($"Failed to add auth token {authTokenCreate}.", e);
        }
    }

    public async Task<AuthToken> GetAuthTokenByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var authToken = await _authTokenRepository.FindAuthTokenByRefreshTokenAsync(refreshToken);
            if (authToken is null || authToken.ExpiresAtUtc > DateTime.UtcNow)
            {
                _logger.LogInformation("Refresh token {RefreshToken} not found.", refreshToken);
                throw new AuthTokenNotFoundServiceException($"Refresh token {refreshToken} not found.");
            }
            
            return authToken;
        }
        catch (Exception e) when (e is AuthTokenNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find refresh token with value {RefreshToken}.", refreshToken);
            throw new AuthTokenServiceException($"Failed to find refresh token with value {refreshToken}.", e);
        }
    }

    public async Task RevokeRefreshTokenByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogInformation("Null or empty refresh token {RefreshToken}.", refreshToken);
                throw new AuthTokenNullOrEmptyServiceException($"Null or empty refresh token {refreshToken}.");
            }
            
            await _authTokenRepository.DeleteAuthTokenByRefreshTokenAsync(refreshToken);
        }
        catch (AuthTokenNotFoundRepositoryException e)
        {
            _logger.LogInformation("Auth token with value {RefreshToken} not found.", refreshToken);
            throw new AuthTokenNotFoundServiceException($"Auth token with value {refreshToken} not found.", e);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to revoke refresh token with value {RefreshToken}.", refreshToken);
            throw new AuthTokenServiceException($"Failed to revoke refresh token with value {refreshToken}.", e);
        }
    }

    public async Task<bool> IsRefreshTokenValidByIdAsync(Guid id)
    {
        try
        {
            var token = await _authTokenRepository.FindAuthTokenByIdAsync(id);
            
            return token is not null && token.ExpiresAtUtc > DateTime.UtcNow;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to valid refresh token with id {Id}.", id);
            throw new AuthTokenServiceException($"Failed to valid refresh token with id {id}.", e);
        }
    }
}