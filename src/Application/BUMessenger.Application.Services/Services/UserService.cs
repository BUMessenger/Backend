using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
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
            var actualApproveCode =
                await _unregisteredUserRepository.FindUnregisteredUserApproveCodeByEmailAsync(userCreate.Email);
            if (actualApproveCode is null)
            {
                _logger.LogInformation("Unregistered user with email = {Email} not found.", userCreate.Email);
                throw new UnregisteredUserNotFoundServiceException(
                    $"Unregistered user with email = {userCreate.Email} not found.");
            }

            if (actualApproveCode != userCreate.ApproveCode)
            {
                _logger.LogInformation("Wrong approve code = {ApproveCode}.", userCreate.ApproveCode);
                throw new WrongApproveCodeUserServiceException($"Wrong approve code = {userCreate.ApproveCode}.");
            }

            var passwordHashed =
                await _unregisteredUserRepository.FindUnregisteredUserPasswordByEmailAsync(userCreate.Email);

            var userCreateWithPassword = userCreate.ToUserCreateWithPassword(passwordHashed!);

            return await _userRepository.AddUserAsync(userCreateWithPassword);
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
}