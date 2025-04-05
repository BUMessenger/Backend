using BUMeesenger.Domain.Exceptions.Repositories.ChatExceptions;
using BUMeesenger.Domain.Exceptions.Services.ChatServiceException;
using BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Chats;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class ChatService(IChatRepository chatRepository,
    IUserRepository userRepository,
    ILogger<ChatService> logger) : IChatService
{
    private readonly IChatRepository _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<ChatService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<Chat> CreateChatAsync(ChatCreate chatCreate)
    {
        try
        {
            await ThrowIfSomeUsersNotExist(chatCreate.UsersIds);
            return await _chatRepository.CreateChatAsync(chatCreate);
        }
        catch (Exception e) when (e is UserNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create chat {@ChatCreate}.", chatCreate);
            throw new ChatServiceException($"Failed to create chat {chatCreate}.", e);
        }
    }

    //todo подумать как можно переписать репозиторий этого метода (возможно вообще перепроектировать)
    public async Task<Paged<ChatSummary>> GetChatsByUserIdAsync(Guid userId, PageFilters filters)
    {
        try
        {
            var chats = await _chatRepository.GetChatsByUserIdAsync(userId, filters);
            return chats;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get chats by user ID {UserId}.", userId);
            throw new ChatServiceException($"Failed to get chats by user ID {userId}.", e);
        }
    }

    public async Task<Chat> GetChatByIdAsync(Guid chatId)
    {
        try
        {
            var chat = await _chatRepository.FindChatByIdAsync(chatId);
            if (chat == null)
            {
                _logger.LogInformation("Chat with id = {Id} wasn't found.", chatId);
                throw new ChatNotFoundServiceException($"User with id = {chatId} wasn't found.");
            }

            return chat;
        }
        catch (Exception e) when (e is ChatNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get chat by ID {ChatId}.", chatId);
            throw new ChatServiceException($"Failed to get chat by ID {chatId}.", e);
        }
    }

    public Task<Chat> UpdateChatNameAsync(Guid chatId, ChatNameUpdate chatNameUpdate)
    {
        try
        {
            return _chatRepository.UpdateChatNameAsync(chatId, chatNameUpdate);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update name of chat with id {@Id}", chatId);
            throw new ChatRepositoryException($"Failed to update name of chat with id {chatId}", e);
        }
    }

    public async Task AddUserToChatAsync(Guid chatId, Guid userId)
    {
        try
        {
            await ThrowIfUserNotExist(userId);
            await _chatRepository.AddUserToChatAsync(chatId, userId);
        }
        catch (Exception e) when (e is UserNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add user with id {UserId} to chat with id {ChatId}.", userId, chatId);
            throw new ChatServiceException($"Failed to add user with id {userId} to chat with id {chatId}.", e);
        }
    }

    public Task RemoveUserFromChatAsync(Guid chatId, Guid userId)
    {
        try
        {
            return _chatRepository.RemoveUserFromChatAsync(chatId, userId);
        }
        catch (Exception e) when (e is UserNotFoundServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to remove user with id {UserId} from chat with id {ChatId}.", userId, chatId);
            throw new ChatServiceException($"Failed to remove user with id {userId} from chat with id {chatId}.", e);
        }
    }


    private async Task ThrowIfSomeUsersNotExist(List<Guid> userIds)
    {
        var checkTasks = userIds.Select(id => _userRepository.IsUserExistByIdAsync(id)).ToList();
        var results = await Task.WhenAll(checkTasks);

        var missingUserIds = userIds
            .Zip(results, (id, exists) => new { id, exists })
            .Where(x => !x.exists)
            .Select(x => x.id)
            .ToList();

        if (missingUserIds.Count != 0)
        {
            _logger.LogError("Users with IDs {MissingUsers} not found.", missingUserIds);
            throw new UserNotFoundServiceException($"Users with IDs {string.Join(", ", missingUserIds)} not found.");
        }
    }
    
    private async Task ThrowIfUserNotExist(Guid userId)
    {
        if (!await _userRepository.IsUserExistByIdAsync(userId))
        {
            _logger.LogError("User with ID {UserId} not found.", userId);
            throw new UserNotFoundServiceException($"User with ID {userId} not found.");
        }
    }
}