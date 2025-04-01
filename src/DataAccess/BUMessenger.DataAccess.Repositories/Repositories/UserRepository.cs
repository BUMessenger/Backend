using BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.DataAccess.Repositories.Extensions;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Users;
using Microsoft.EntityFrameworkCore;
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
            _logger.LogError(e, "Failed to add user {UserCreate}.", userCreate);
            throw new UserRepositoryException($"Failed to add user {userCreate}.", e);
        }
    }

    public async Task<bool> IsUserExistByEmailAsync(string email)
    {
        try
        {
            var usersCount = await _context.Users
                .Where(u => u.Email == email)
                .AsNoTracking()
                .CountAsync();

            return usersCount > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find users with email {@Email}", email);
            throw new UserRepositoryException($"Failed to find users with email {email}", e);
        }
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        try
        {
            var userDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return userDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find user with email {@Email}", email);
            throw new UserRepositoryException($"Failed to find user with email {email}", e);
        }
    }

    public async Task<User?> FindUserByIdAsync(Guid id)
    {
        try
        {
            var userDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return userDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find user with id {@Id}", id);
            throw new UserRepositoryException($"Failed to find user with id {id}", e);
        }
    }
    
    public async Task<bool> IsPasswordMatchAsync(Guid userId, string password)
    {
        try
        {
            var realPassword = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => u.PasswordHashed)
                .FirstOrDefaultAsync();
            
            return realPassword == password;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find user with id {@Id}", userId);
            throw new UserRepositoryException($"Failed to find user with id {userId}", e);
        }
    }

    public async Task<Users> GetUsersByFiltersAsync(UserFilters userFilters, PageFilters pageFilters)
    {
        try
        {
            var usersDbQueryable = _context.Users
                .Include(u => u.ChatUserInfos)
                .AsNoTracking()
                .ApplyUserFilters(userFilters);

            var usersDb = await usersDbQueryable
                .Skip(pageFilters.Skip)
                .Take(pageFilters.Limit)
                .ToListAsync();

            return new Users
            {
                Count = usersDbQueryable.Count(),
                Items = usersDb.ConvertAll(UserConverterDb.ToDomain)!,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get users by filters {@UserFilters}", userFilters);
            throw new UserRepositoryException($"Failed to get users by filters {userFilters}", e);
        }
    }
}