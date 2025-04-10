using BUMeesenger.Domain.Exceptions.Services.EmailServiceExceptions;
using BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Application.Services.Helpers;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models.Converters;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class UnregisteredUserService : IUnregisteredUserService
{
    private readonly IUnregisteredUserRepository _unregisteredUserRepository;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UnregisteredUserService> _logger;

    public UnregisteredUserService(IUnregisteredUserRepository unregisteredUserRepository,
        IEmailService emailService,
        IUserRepository userRepository,
        ILogger<UnregisteredUserService> logger)
    {
        _unregisteredUserRepository = unregisteredUserRepository ?? throw new ArgumentNullException(nameof(unregisteredUserRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UnregisteredUser> AddUnregisteredUserAsync(UnregisteredUserCreate unregisteredUser)
    {
        try
        {
            await ThrowIfUnregisteredUserWithSameEmailExists(unregisteredUser.Email);
            await ThrowIfUserWithSameEmailExists(unregisteredUser.Email);
            
            var passwordHashed = HashHelper.ComputeMD5Hash(unregisteredUser.Password);

            var approveCode = GenerateApproveCode();
            var emailBody = "Код подтверждения: " + approveCode;
            
            await _emailService.SendEmailAsync(unregisteredUser.Email, 
                "Код подтверждения для регистрации в BUMessenger",
                emailBody);
            
            var unregisteredUserCreate = unregisteredUser.ToDomainWithAdditionalData(passwordHashed,
                approveCode);
            
            return await _unregisteredUserRepository.AddUnregisteredUserAsync(unregisteredUserCreate);
        }
        catch (Exception e) when (e is UnregisteredUserAlreadyExistsServiceException
                                  or ReceiverDoesntExistEmailServiceException
                                  or EmailServiceException
                                  or UserAlreadyExistsServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add unregistered user {@UnregisteredUser}", unregisteredUser);
            throw new UnregisteredUserServiceException($"Failed to add unregistered user {unregisteredUser}", e);
        }
    }

    private string GenerateApproveCode()
    {
        var rnd = new Random();
        
        var value = rnd.Next(100000, 999999);

        return value.ToString();
    }

    private async Task ThrowIfUnregisteredUserWithSameEmailExists(string email)
    {
        if (await _unregisteredUserRepository.IsUnregisteredUserExistByEmailAsync(email))
        {
            _logger.LogInformation("Unregistered user with email {Email} already exists.", email);
            throw new UnregisteredUserAlreadyExistsServiceException(
                $"Unregistered user with email {email} already exists.");
        }
    }
    
    private async Task ThrowIfUserWithSameEmailExists(string email)
    {
        if (await _userRepository.IsUserExistByEmailAsync(email))
        {
            _logger.LogInformation("User with email {Email} already exists.", email);
            throw new UserAlreadyExistsServiceException(
                $"User with email {email} already exists.");
        }
    }
}