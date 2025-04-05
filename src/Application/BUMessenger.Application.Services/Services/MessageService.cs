using BUMeesenger.Domain.Exceptions.Services.MessageServiceException;
using BUMessenger.Domain.Interfaces.Repositories;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Messages;
using Microsoft.Extensions.Logging;

namespace BUMessenger.Application.Services.Services;

public class MessageService(IMessageRepository messageRepository,
    ILogger<MessageService> logger) : IMessageService
{
    private readonly IMessageRepository _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
    private readonly ILogger<MessageService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<Paged<Message>> GetMessagesAsync(Guid chatId, PageFilters filters)
    {
        try
        {
            return await _messageRepository.GetMessagesAsync(chatId, filters);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get messages from chat with id {@Id}.", chatId);
            throw new MessageServiceException($"Failed to get messages from chat with id {chatId}.", e);
        }
    }

    public async Task<Paged<Message>> GetThreadMessagesAsync(Guid chatId, Guid parentMessageId, PageFilters filters)
    {
        try
        {
            return await _messageRepository.GetThreadMessagesAsync(chatId, parentMessageId, filters);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get thread messages, created from message with id {@Id}", 
                parentMessageId);
            throw new MessageServiceException($"Failed to get thread messages, " +
                                                 $"created from message with id {parentMessageId}", e);
        }
    }
}