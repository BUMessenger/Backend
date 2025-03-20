using BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models.Users;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BUMessengerContext _context;
    private readonly ILogger<IUserRepository> _logger;

    public UserRepository(BUMessengerContext context, ILogger<IUserRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User> AddUserAsync(UserCreateWithPassword userCreate)
    {
        try
        {
            var userDb = userCreate.ToDb();
            
            var addedUser = await _context.Users.AddAsync(userDb);
            await _context.SaveChangesAsync();
            
            return addedUser.Entity.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to add user {UserCreate}.", userCreate);
            throw new UserRepositoryException($"Failed to add user {userCreate}.", e);
        }
    }
}