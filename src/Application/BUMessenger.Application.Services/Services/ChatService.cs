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
    
    public async Task<Chat> CreateChatAsync(Guid creatorId, ChatCreate chatCreate)
    {
        try
        {
            await ThrowIfSomeUsersNotExist(chatCreate.UsersIds);
            
            if (chatCreate.UsersIds.Contains(creatorId))
            {
                chatCreate.UsersIds.Remove(creatorId);
            }
            
            if (string.IsNullOrWhiteSpace(chatCreate.ChatName))
            {
                _logger.LogError("Failed to create chat {@ChatCreate} for user with id {CreatorId}. " +
                                 "Chat name cannot be empty.", chatCreate, creatorId);
                throw new EmptyChatNameServiceException($"Failed to create chat {chatCreate} for user with id {creatorId}." +
                                            "Chat name cannot be empty.");
            }
            
            return await _chatRepository.CreateChatAsync(creatorId, chatCreate);
        }
        catch (Exception e) when (e is UserNotFoundServiceException
                                  or EmptyChatNameServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create chat {@ChatCreate}.", chatCreate);
            throw new ChatServiceException($"Failed to create chat {chatCreate}.", e);
        }
    }
    
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
    
    public async Task<Chat> GetChatByIdAsync(Guid chatId, Guid userId)
    {
        try
        {
            var chat = await _chatRepository.FindChatByIdAsync(chatId);
            if (chat == null)
            {
                _logger.LogInformation("Chat with id = {Id} wasn't found.", chatId);
                throw new ChatNotFoundServiceException($"Chat with id = {chatId} wasn't found.");
            }
            
            if (!await _chatRepository.UserInChatAsync(chatId, userId))
            {
                _logger.LogError("User with id {UserId} is not in chat with id {ChatId}.", userId, chatId);
                throw new UserNotInChatServiceException($"User with id {userId} is not in chat with id {chatId}.");
            }

            return chat;
        }
        catch (Exception e) when (e is ChatNotFoundServiceException
                                  or UserNotInChatServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get chat by ID {ChatId}.", chatId);
            throw new ChatServiceException($"Failed to get chat by ID {chatId}.", e);
        }
    }
    
    public async Task<Chat> UpdateChatNameAsync(Guid chatId, Guid userId, ChatNameUpdate chatNameUpdate)
    {
        try
        {
            await ThrowIfUserNotInChat(chatId, userId);
            
            if (string.IsNullOrWhiteSpace(chatNameUpdate.ChatName))
            {
                _logger.LogError("Failed to update name of chat {@ChatNameUpdate} for user with id {UserId}. " +
                                 "Chat name cannot be empty.", chatNameUpdate, userId);
                throw new EmptyChatNameServiceException($"Failed to update name of chat {chatNameUpdate} for user with id {userId}." +
                                                        "Chat name cannot be empty.");
            }
            
            return await _chatRepository.UpdateChatNameAsync(chatId, chatNameUpdate);
        }
        catch (Exception e) when (e is UserNotInChatServiceException
                                  or EmptyChatNameServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update name of chat with id {@Id}", chatId);
            throw new ChatServiceException($"Failed to update name of chat with id {chatId}", e);
        }
    }
    
    public async Task AddUserToChatAsync(Guid chatId, Guid userId, Guid userAddId)
    {
        try
        {
            await ThrowIfUserNotExist(userAddId);
            await ThrowIfUserNotInChat(chatId, userId);
            
            await _chatRepository.AddUserToChatAsync(chatId, userAddId);
        }
        catch (Exception e) when (e is UserNotFoundServiceException 
                                      or UserNotInChatServiceException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add user with id {UserId} to chat with id {ChatId}.", userId, chatId);
            throw new ChatServiceException($"Failed to add user with id {userId} to chat with id {chatId}.", e);
        }
    }

    public async Task RemoveUserFromChatAsync(Guid chatId, Guid userId)
    {
        try
        {
            await _chatRepository.RemoveUserFromChatAsync(chatId, userId);
        }
        catch (Exception e) when (e is ChatNotFoundRepositoryException)
        {
            throw new ChatNotFoundServiceException($"Failed to find user with id {userId}" +
                                                   $" in chat with id {chatId}");
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to remove user with id {UserId} from chat with id {ChatId}.", userId, chatId);
            throw new ChatServiceException($"Failed to remove user with id {userId} from chat with id {chatId}.", e);
        }
    }
    
    private async Task ThrowIfSomeUsersNotExist(List<Guid> userIds)
    {
        var notExistUsers = new List<Guid>();
        foreach (var userId in userIds)
        {
            if (!await _userRepository.IsUserExistByIdAsync(userId))
            {
                notExistUsers.Add(userId);
            }
        }

        if (notExistUsers.Count > 0)
        {
            _logger.LogError("Users with IDs {UserIds} not found.", string.Join(", ", notExistUsers));
            throw new UserNotFoundServiceException($"Users with IDs {string.Join(", ", notExistUsers)} not found.");
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

    private async Task ThrowIfUserNotInChat(Guid chatId, Guid userId)
    {
        if (!await _chatRepository.UserInChatAsync(chatId, userId))
        {
            _logger.LogError("User with id {UserId} is not in chat with id {ChatId}.", userId, chatId);
            throw new UserNotInChatServiceException($"User with id {userId} is not in chat with id {chatId}.");
        }
    }
}