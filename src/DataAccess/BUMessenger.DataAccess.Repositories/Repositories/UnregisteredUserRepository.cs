using BUMeesenger.Domain.Exceptions.Repositories.UnregisteredUserExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class UnregisteredUserRepository : IUnregisteredUserRepository
{
    private readonly BUMessengerContext _context;
    private readonly ILogger<IUnregisteredUserRepository> _logger;

    public UnregisteredUserRepository(BUMessengerContext context,
        ILogger<IUnregisteredUserRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<UnregisteredUser> AddUnregisteredUserAsync(UnregisteredUserCreateWithAdditionalData unregisteredUser)
    {
        try
        {
            var unregisteredUserDb = unregisteredUser.ToDb();
            
            var addedUnregisteredUser = await _context.UnregisteredUsers.AddAsync(unregisteredUserDb);
            await _context.SaveChangesAsync();
            
            return addedUnregisteredUser.Entity.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to add unregistered user {@UnregisteredUser}", unregisteredUser);
            throw new UnregisteredUserRepositoryException($"Failed to add unregistered user {unregisteredUser}", e);
        }
    }

    public async Task<string?> FindUnregisteredUserApproveCodeByEmailAsync(string email)
    {
        try
        {
            var approveCode = await _context.UnregisteredUsers
                .Where(u => u.Email == email)
                .AsNoTracking()
                .Select(u => u.ApproveCode)
                .FirstOrDefaultAsync();
            
            return approveCode;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find approve code for user with email {@Email}", email);
            throw new UnregisteredUserRepositoryException($"Failed to find approve code for user with email {email}", e);
        }
    }

    public async Task<bool> IsUnregisteredUserExistByEmailAsync(string email)
    {
        try
        {
            var unregisteredUser = await _context.UnregisteredUsers
                .Where(u => u.Email == email)
                .AsNoTracking()
                .CountAsync();
            
            return unregisteredUser > 0;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find user with email {@Email}", email);
            throw new UnregisteredUserRepositoryException($"Failed to find user with email {email}", e);
        }
    }

    public async Task<string?> FindUnregisteredUserPasswordByEmailAsync(string email)
    {
        try
        {
            var passwordHashed = await _context.UnregisteredUsers
                .Where(u => u.Email == email)
                .AsNoTracking()
                .Select(u => u.PasswordHashed)
                .FirstOrDefaultAsync();
            
            return passwordHashed;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find user with email {@Email}", email);
            throw new UnregisteredUserRepositoryException($"Failed to find user with email {email}", e);
        }
    }
}