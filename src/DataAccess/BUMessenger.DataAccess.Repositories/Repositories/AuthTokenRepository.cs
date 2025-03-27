using BUMeesenger.Domain.Exceptions.Repositories.AuthTokenExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models.AuthTokens;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class AuthTokenRepository : IAuthTokenRepository
{
    private readonly BUMessengerContext _context;
    private readonly ILogger<IAuthTokenRepository> _logger;

    public AuthTokenRepository(BUMessengerContext context, ILogger<IAuthTokenRepository> logger)
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
            _logger.LogError("Failed to add auth token {AuthToken}.", authTokenCreate);
            throw new AuthTokenRepositoryException($"Failed to add auth token {authTokenCreate}.", e);
        }
    }
}