using BUMeesenger.Domain.Exceptions.Repositories.ChatExceptions;
using BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Chats;
using BUMessenger.Domain.Models.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class ChatRepository(BUMessengerContext context,
    ILogger<ChatRepository> logger) : IChatRepository 
{
    private readonly BUMessengerContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<ChatRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<Chat> CreateChatAsync(Guid creatorId, ChatCreate chatCreate)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        { 
            var chatDb = new ChatDb(id: Guid.NewGuid(),
                chatName: chatCreate.ChatName);
            
            var userChatInfos = chatCreate.UsersIds
                .Select(userId => new ChatUserInfoDb(Guid.NewGuid(), chatDb.Id, userId, null))
                .ToList();
            
            userChatInfos.Add(new ChatUserInfoDb(Guid.NewGuid(), chatDb.Id, creatorId, null));
            
            await _context.ChatUserInfos.AddRangeAsync(userChatInfos);
            
            await _context.Chats.AddAsync(chatDb);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            var fullChatDb = await _context.Chats
                .Include(c => c.Users)
                .FirstAsync(c => c.Id == chatDb.Id);
            
            return fullChatDb.ToDomain();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, "Failed to create chat {@ChatCreate} for creator {CreatorId}.", chatCreate, creatorId);
            throw new ChatRepositoryException($"Failed to create chat {chatCreate} for creator {creatorId}.", e);
        }
    }

    public async Task<Paged<ChatSummary>> GetChatsByUserIdAsync(Guid userId, PageFilters filters)
    {
        try
        {
            var query = _context.ChatUserInfos
                .Include(cui => cui.Chat)
                .Where(cui => cui.UserId == userId)
                .Select(cui => new ChatSummaryDb
                {
                    Id = cui.Chat!.Id,
                    ChatName = cui.Chat.ChatName,
                    LastMessage = cui.Chat.Messages
                        .OrderByDescending(m => m.SentAtUtc)
                        .FirstOrDefault(),
                    UnreadMessagesNumber = cui.Chat.Messages
                        .Count(m => m.SentAtUtc > (cui.LastReadMessage != null
                            ? cui.LastReadMessage.SentAtUtc
                            : DateTime.MinValue))
                });
            
            var chatsPreviews = await query
                .OrderByDescending(c => c.LastMessage != null ? c.LastMessage.SentAtUtc : DateTime.MinValue)
                .Skip(filters.Skip)
                .Take(filters.Limit)
                .ToListAsync();
            
            var count = await query.CountAsync();

            return new Paged<ChatSummary> {Count = count, Items = chatsPreviews.ConvertAll(cs => cs.ToDomain())};
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find chats for user with id {@Id}", userId);
            throw new ChatRepositoryException($"Failed to find chats for user with id {userId}", e);
        }
    }

    public async Task<Chat?> FindChatByIdAsync(Guid chatId)
    {
        try
        {
            var chatDb = await _context.Chats
                .Include(c => c.Users)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == chatId);
            
            return chatDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find chat with id {@Id}", chatId);
            throw new ChatRepositoryException($"Failed to find chat with id {chatId}", e);
        }
    }

    public async Task<Chat> UpdateChatNameAsync(Guid chatId, ChatNameUpdate chatNameUpdate)
    {
        try
        {
            var chatDb = await _context.Chats
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == chatId);

            if (chatDb is null)
            {
                _logger.LogError("Failed to find chat with id {@Id}", chatId);
                throw new ChatNotFoundRepositoryException($"Failed to find chat with id {chatId}");
            }

            chatDb.ChatName = chatNameUpdate.ChatName;
            await _context.SaveChangesAsync();

            return chatDb.ToDomain();
        }
        catch (Exception e) when (e is ChatNotFoundRepositoryException)
        {
            throw;
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
            var chatDb = await _context.Chats
                .Include(c => c.ChatUserInfos)
                .FirstOrDefaultAsync(c => c.Id == chatId);

            if (chatDb is null)
            {
                _logger.LogError("Failed to find chat with id {@Id}", chatId);
                throw new ChatNotFoundRepositoryException($"Failed to find chat with id {chatId}");
            }
            
            if (chatDb.ChatUserInfos.Any(cui => cui.UserId == userId))
            {
                _logger.LogInformation("User {UserId} is already a member of chat {ChatId}.", userId, chatId);
                return;
            }

            var userChatInfo = new ChatUserInfoDb(id: Guid.NewGuid(),
                chatId: chatId,
                userId: userId,
                lastReadMessageId: null);
            
            await _context.ChatUserInfos.AddAsync(userChatInfo);
            await _context.SaveChangesAsync();
        }
        catch (Exception e) when (e is ChatNotFoundRepositoryException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add user with id {@UserId} to chat with id {@ChatId}", 
                userId, 
                chatId);
            throw new ChatRepositoryException($"Failed to add user with id {userId} to chat with id {chatId}", e);
        }
    }

    public async Task RemoveUserFromChatAsync(Guid chatId, Guid userId)
    {
        try
        {
            var deletedCount = await _context.ChatUserInfos
                .Where(cui => cui.ChatId == chatId && cui.UserId == userId)
                .ExecuteDeleteAsync();

            if (deletedCount == 0)
            {
                _logger.LogError("Failed to find user with id {@UserId} in chat with id {@ChatId}", 
                    userId, 
                    chatId);
                throw new ChatNotFoundRepositoryException($"Failed to find user with id {userId}" +
                                                          $" in chat with id {chatId}");
            }
        }
        catch (Exception e) when (e is ChatNotFoundRepositoryException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to remove user with id {@UserId} from chat with id {@ChatId}", 
                userId, 
                chatId);
            throw new ChatRepositoryException($"Failed to remove user with id {userId}" +
                                              $" from chat with id {chatId}");
        }
    }
    
    public async Task<bool> UserInChatAsync(Guid chatId, Guid userId)
    {
        try
        {
            return await _context.ChatUserInfos
                .AnyAsync(cui => cui.ChatId == chatId && cui.UserId == userId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to check if user with id {@UserId} is in chat with id {@ChatId}", 
                userId, 
                chatId);
            throw new ChatRepositoryException($"Failed to check if user with id {userId}" +
                                              $" is in chat with id {chatId}", e);
        }
    }
}
