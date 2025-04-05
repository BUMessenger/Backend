using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Messages;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IMessageService
{
    /// <summary>
    /// Получение сообщений чата
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="filters">Фильтры пагинации</param>
    /// <returns>Коллекция сообщений</returns>
    Task<Paged<Message>> GetMessagesAsync(Guid chatId, PageFilters filters);
    
    /// <summary>
    /// Получение сообщений треда
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="parentMessageId">Идентификатор родительского сообщения</param>
    /// <param name="filters">Фильтры пагинации</param>
    /// <returns>Коллекция сообщений</returns>
    Task<Paged<Message>> GetThreadMessagesAsync(Guid chatId, Guid parentMessageId, PageFilters filters);
}