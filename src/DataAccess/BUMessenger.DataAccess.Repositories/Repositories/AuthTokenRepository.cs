using BUMeesenger.Domain.Exceptions.Repositories.AuthTokenExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models.AuthTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class AuthTokenRepository : IAuthTokenRepository
{
    private readonly BUMessengerContext _context;
    private readonly ILogger<AuthTokenRepository> _logger;

    public AuthTokenRepository(BUMessengerContext context, ILogger<AuthTokenRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AuthToken> AddAuthTokenAsync(AuthTokenCreate authTokenCreate)
    {
        try
        {
            var authTokenDb = authTokenCreate.ToDb();
            
            var addedAuthToken = await _context.AuthTokens.AddAsync(authTokenDb);
            await _context.SaveChangesAsync();
            
            return addedAuthToken.Entity.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add auth token {AuthToken}.", authTokenCreate);
            throw new AuthTokenRepositoryException($"Failed to add auth token {authTokenCreate}.", e);
        }
    }

    public async Task<AuthToken?> FindAuthTokenByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var authTokenDb = await _context.AuthTokens
                .Where(a => a.RefreshToken == refreshToken)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return authTokenDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find refresh token with value {RefreshToken}.", refreshToken);
            throw new AuthTokenRepositoryException($"Failed to find refresh token with value {refreshToken}.", e);
        }
    }

    public async Task DeleteAuthTokenByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var count = await _context.AuthTokens
                .Where(a => a.RefreshToken == refreshToken)
                .ExecuteDeleteAsync();

            if (count == 0)
            {
                _logger.LogInformation("Auth token with value {RefreshToken} not found.", refreshToken);
                throw new AuthTokenNotFoundRepositoryException($"Auth token with value {refreshToken} not found.");
            }
        }
        catch (AuthTokenNotFoundRepositoryException e)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete refresh token with value {RefreshToken}.", refreshToken);
            throw new AuthTokenRepositoryException($"Failed to delete refresh token with value {refreshToken}.", e);
        }
    }

    public async Task<AuthToken?> FindAuthTokenByIdAsync(Guid id)
    {
        try
        {
            var authTokenDb = await _context.AuthTokens
                .Where(a => a.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return authTokenDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find refresh token with id {Id}.", id);
            throw new AuthTokenRepositoryException($"Failed to find refresh token with id {id}.", e);
        }
    }
}