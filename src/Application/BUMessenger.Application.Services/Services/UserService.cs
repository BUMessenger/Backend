using BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;
using BUMeesenger.Domain.Exceptions.Services.EmailServiceExceptions;
using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Application.Services.Helpers;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Converters;
using BUMessenger.Domain.Models.Models.Users;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnregisteredUserRepository _unregisteredUserRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<IUserService> _logger;

    public UserService(IUserRepository userRepository,
        IUnregisteredUserRepository unregisteredUserRepository,
        IEmailService emailService,
        ILogger<IUserService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unregisteredUserRepository = unregisteredUserRepository ?? throw new ArgumentNullException(nameof(unregisteredUserRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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

            if (unregisteredUser.ApproveCode != userCreate.ApproveCode)
            {
                _logger.LogInformation("Wrong approve code = {ApproveCode}.", userCreate.ApproveCode);
                throw new WrongApproveCodeUserServiceException($"Wrong approve code = {userCreate.ApproveCode}.");
            }

            if (unregisteredUser.ExpiresAtUtc < DateTime.UtcNow)
            {
                _logger.LogInformation("Expired approve code = {ApproveCode}.", userCreate.ApproveCode);
                throw new ExpiredApproveCodeUserServiceException($"Expired approve code = {userCreate.ApproveCode}.");
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
            _logger.LogError(e, "Failed to add user {UserCreate}.", userCreate);
            throw new UserServiceException($"Failed to add user {userCreate}.", e);
        }
    }

    public async Task<User> AuthUserByEmailPasswordAsync(string email, string password)
    {
        try
        {
            var user = await _userRepository.FindUserByEmailAsync(email);
            if (user is null)
            {
                _logger.LogInformation("User with email = {Email} wasn't found.", email);
                throw new UserNotFoundServiceException($"User with email = {email} wasn't found.");
            }
            
            var passwordHashed = HashHelper.ComputeMD5Hash(password);
            
            if (! await _userRepository.IsPasswordMatchAsync(user.Id, passwordHashed))
            {
                _logger.LogInformation("Wrong password for user with email = {Email}.", email);
                throw new UserWrongPasswordServiceException($"Wrong password for user with email = {email}.");
            }
            
            return user;
        }
        catch (Exception e) when (e is UserNotFoundServiceException
                                  or UserWrongPasswordServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authorize user with email {@Email}", email);
            throw new UserServiceException($"Failed to authorize user with email {email}", e);
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
        catch (Exception e) when (e is UserNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find user with id {@Id}", id);
            throw new UserServiceException($"Failed to find user with id {id}", e);
        }
    }

    public async Task<Users> GetUsersByFiltersAsync(UserFilters userFilters, PageFilters pageFilters)
    {
        try
        {
            return await _userRepository.GetUsersByFiltersAsync(userFilters, pageFilters);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get users by filters {@UserFilters}", userFilters);
            throw new UserServiceException($"Failed to get users by filters {userFilters}", e);
        }
    }

    public async Task RecoveryUserPasswordAsync(UserPasswordRecovery userPasswordRecovery)
    {
        try
        {
            var user = await _userRepository.FindUserByEmailAsync(userPasswordRecovery.Email);
            if (user is null)
            {
                _logger.LogInformation("User with email = {Email} wasn't found.", userPasswordRecovery.Email);
                throw new UserNotFoundServiceException($"User with email = {userPasswordRecovery.Email} wasn't found.");
            }

            var newPassword = GeneratePasswordHelper.GenerateRandomString();

            await _emailService.SendEmailAsync(userPasswordRecovery.Email,
                "Сброс пароля в BUMessenger",
                $"Ваш новый пароль для входа в BUMessenger: {newPassword}");

            var passwordHashed = HashHelper.ComputeMD5Hash(newPassword);

            await _userRepository.UpdatePasswordByIdAsync(user.Id, passwordHashed);
        }
        catch (Exception e) when (e is UserNotFoundServiceException
                                      or ReceiverDoesntExistEmailServiceException)
        {
            throw;
        }
        catch (UserNotFoundRepositoryException)
        {
            _logger.LogInformation("User with email = {Email} not found.", userPasswordRecovery.Email);
            throw new UserNotFoundServiceException($"User with email = {userPasswordRecovery.Email} not found.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to recovery password for user with email = {Email}", userPasswordRecovery.Email);
            throw new UserServiceException($"Failed to recovery password for user with email = {userPasswordRecovery.Email}", e);
        }
    }

    public async Task DeleteUserByIdAsync(Guid id)
    {
        try
        {
            await _userRepository.DeleteUserByIdAsync(id);
        }
        catch (UserNotFoundRepositoryException)
        {
            _logger.LogInformation("User with id = {Id} not found.", id);
            throw new UserNotFoundServiceException($"User with id = {id} not found.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete user by id = {Id}", id);
            throw new UserServiceException($"Failed to delete user by id = {id}", e);
        }
    }
}