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
}