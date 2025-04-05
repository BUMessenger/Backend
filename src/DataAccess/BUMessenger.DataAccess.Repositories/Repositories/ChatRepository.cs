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
    
    public async Task<Chat> CreateChatAsync(ChatCreate chatCreate)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingUsers = await _context.Users
                .Where(u => chatCreate.UsersIds.Contains(u.Id))
                .Select(u => u.Id)
                .ToListAsync();
            
            var missingUsers = chatCreate.UsersIds.Except(existingUsers).ToList();
            
            if (missingUsers.Count != 0)
            {
                _logger.LogError("Users with IDs {MissingUsers} not found.", missingUsers);
                throw new ArgumentException($"Users with IDs {string.Join(", ", missingUsers)} not found.");
            }
            
            var chatDb = new ChatDb(id: Guid.NewGuid(),
                chatName: chatCreate.ChatName);
            
            foreach (var userId in chatCreate.UsersIds)
            {
                var userChatInfo = new ChatUserInfoDb(Guid.NewGuid(), chatDb.Id, userId, null);
                await _context.ChatUserInfos.AddAsync(userChatInfo);
            }
            
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
            _logger.LogError(e, "Failed to create chat {@ChatCreate}.", chatCreate);
            throw new ChatRepositoryException($"Failed to create chat {chatCreate}.", e);
        }
    }

    public async Task<Paged<ChatSummary>> GetChatsByUserIdAsync(Guid userId, PageFilters filters)
    {
        try
        {
            //todo тут загружаются все сообщения, это очень неэффективно
            //todo еще раз проверить этот метод, он выглядит неэффективным
            var query = _context.Chats
                .Where(c => c.ChatUserInfos.Any(cui => cui.UserId == userId))
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .Include(c => c.ChatUserInfos)
                .AsNoTracking();
        
            var chats = await query
                .OrderByDescending(c => c.Messages.Max(m => m.SentAtUtc))
                .Skip(filters.Skip)
                .Take(filters.Limit)
                .ToListAsync();
        
            var count = await query.CountAsync();
        
            var items = chats.Select(chat =>
            {
                var lastMessage = chat.Messages.OrderByDescending(m => m.SentAtUtc).FirstOrDefault();
                var userChatInfo = chat.ChatUserInfos.FirstOrDefault(cui => cui.UserId == userId);
            
                int unreadMessagesCount = 0;
                if (userChatInfo != null)
                {
                    unreadMessagesCount = chat.Messages
                        .Count(m => m.SentAtUtc > 
                                    (userChatInfo.LastReadMessage?.SentAtUtc ?? DateTime.MinValue));
                }
            
                return chat.ToSummaryDomain(lastMessage, unreadMessagesCount);
            }).ToList();
        
            return new Paged<ChatSummary> { Items = items, Count = count };
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
            
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                _logger.LogError("Failed to find user with id {@Id}", userId);
                throw new UserNotFoundRepositoryException($"Failed to find user with id {userId}");
            }
            
            if (chatDb.ChatUserInfos.Any(cui => cui.UserId == userId))
            {
                _logger.LogInformation("User {UserId} is already a member of chat {ChatId}.", userId, chatId);
                return;
            }

            var userChatInfo = new ChatUserInfoDb(Guid.NewGuid(), chatId, userId, null);
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
}