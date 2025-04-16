using BUMeesenger.Domain.Exceptions.Repositories.ChatExceptions;
using BUMeesenger.Domain.Exceptions.Repositories.MessageException;
using BUMessenger.DataAccess.Context;
using BUMessenger.DataAccess.Models.Converters;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BUMessenger.DataAccess.Repositories.Repositories;

public class MessageRepository(BUMessengerContext context,
    ILogger<MessageRepository> logger): IMessageRepository
{
    private readonly BUMessengerContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<MessageRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
     public async Task<Paged<Message>> GetMessagesAsync(Guid chatId, PageFilters filters)
    {
        try
        {
            var query = _context.Messages
                .Where(m => m.ChatId == chatId && m.ParentMessageId == null)
                .AsNoTracking();

            var messages = await query
                .OrderByDescending(m => m.SentAtUtc)
                .Skip(filters.Skip)
                .Take(filters.Limit)
                .ToListAsync();

            var count = await query.CountAsync();
            var items = messages.ConvertAll(m => m.ToDomain());

            return new Paged<Message> { Count = count, Items = items };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get messages of chat with id {@Id}", chatId);
            throw new MessageRepositoryException($"Failed to get messages of chat with id {chatId}", e);
        }
    }

    public async Task<Paged<Message>> GetThreadMessagesAsync(Guid chatId, Guid parentMessageId, PageFilters filters)
    {
        try
        {
            var query = _context.Messages
                .Where(m => m.ChatId == chatId && m.ParentMessageId == parentMessageId)
                .AsNoTracking();

            var messages = await query
                .OrderByDescending(m => m.SentAtUtc)
                .Skip(filters.Skip)
                .Take(filters.Limit)
                .ToListAsync();

            var count = await query.CountAsync();
            var items = messages.ConvertAll(m => m.ToDomain());

            return new Paged<Message> { Count = count, Items = items };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get thread messages, created from message with id {@Id}", 
                parentMessageId);
            throw new MessageRepositoryException($"Failed to get thread messages, " +
                                              $"created from message with id {parentMessageId}", e);
        }
    }
    
    public async Task<Message> CreateMessageAsync(MessageCreate messageCreate)
    {
        try
        {
            var messageDb = new MessageDb(id: Guid.NewGuid(),
                chatId: messageCreate.ChatId,
                creatorId: messageCreate.CreatorId,
                parentMessageId: messageCreate.ParentMessageId,
                sentAtUtc: messageCreate.SentAtUtc,
                messageText: messageCreate.MessageText);
            
            await _context.Messages.AddAsync(messageDb);
            await _context.SaveChangesAsync();

            return messageDb.ToDomain();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create message {@MessageCreate}", messageCreate);
            throw new MessageRepositoryException($"Failed to create message {messageCreate}", e);
        }
    }
}