using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Application.Services.Helpers;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.Converters;
using BUMessenger.Domain.Models.Models.Users;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnregisteredUserRepository _unregisteredUserRepository;
    private readonly ILogger<IUserService> _logger;

    public UserService(IUserRepository userRepository,
        IUnregisteredUserRepository unregisteredUserRepository,
        ILogger<IUserService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unregisteredUserRepository = unregisteredUserRepository ?? throw new ArgumentNullException(nameof(unregisteredUserRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User> AddUserAsync(UserCreate userCreate)
    {
        try
        {
            var unregisteredUser =
                await _unregisteredUserRepository.FindUnregisteredUserByEmailAsync(userCreate.Email);
            if (unregisteredUser is null)
            {
                _logger.LogInformation("Unregistered user with email = {Email} not found.", userCreate.Email);
                throw new UnregisteredUserNotFoundServiceException(
                    $"Unregistered user with email = {userCreate.Email} not found.");
            }

            if (unregisteredUser.ApproveCode != userCreate.ApproveCode ||
                unregisteredUser.ExpiresAtUtc < DateTime.UtcNow)
            {
                _logger.LogInformation("Wrong or expired approve code = {ApproveCode}.", userCreate.ApproveCode);
                throw new WrongApproveCodeUserServiceException($"Wrong or expired approve code = {userCreate.ApproveCode}.");
            }

            var passwordHashed = unregisteredUser.PasswordHashed;

            var userCreateWithPassword = userCreate.ToUserCreateWithPassword(passwordHashed!);

            var addedUser = await _userRepository.AddUserAsync(userCreateWithPassword);
            
            await _unregisteredUserRepository.DeleteUnregisteredUserByEmailAsync(userCreate.Email);
            
            return addedUser;
        }
        catch (Exception e) when (e is UnregisteredUserNotFoundServiceException
                                      or WrongApproveCodeUserServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to add user {UserCreate}.", userCreate);
            throw new UserServiceException($"Failed to add user {userCreate}.", e);
        }
    }

    public async Task<User> AuthUserByEmailPasswordAsync(string email, string password)
    {
        try
        {
            if (!await _userRepository.IsUserExistByEmailAsync(email))
            {
                _logger.LogInformation("User with email = {Email} wasn't found.", email);
                throw new UserNotFoundServiceException($"User with email = {email} wasn't found.");
            }
            
            var passwordHashed = HashHelper.ComputeMD5Hash(password);

            var user = await _userRepository.FindUserByEmailPasswordHashedAsync(email, passwordHashed);
            if (user is null)
            {
                _logger.LogInformation("Wrong password for user with email = {Email}.", email);
                throw new UserWrongPasswordServiceException($"Wrong password for user with email = {email}.");
            }
            
            return user;
        }
        catch (Exception e) when (e is UserNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find user with email {@Email}", email);
            throw new UserServiceException($"Failed to find user with email {email}", e);
        }
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.FindUserByIdAsync(id);
            if (user is null)
            {
                _logger.LogInformation("User with id = {Id} wasn't found.", id);
                throw new UserNotFoundServiceException($"User with id = {id} wasn't found.");
            }
            
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to find user with id {@Id}", id);
            throw new UserServiceException($"Failed to find user with id {id}", e);
        }
    }
}