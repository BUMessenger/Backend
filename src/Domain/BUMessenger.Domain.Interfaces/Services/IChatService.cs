using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Chats;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IChatService
{
    /// <summary>
    /// Создание чата
    /// </summary>
    /// <param name="chatCreate">Модель создания чата</param>
    /// <returns>Созданный чат</returns>
    Task<Chat> CreateChatAsync(ChatCreate chatCreate);
    
    /// <summary>
    /// Получение чатов пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="filters">Фильтры</param>
    /// <returns>Коллекция чатов(сокращенная модель)</returns>
    Task<Paged<ChatSummary>> GetChatsByUserIdAsync(Guid userId, PageFilters filters);
    
    /// <summary>
    /// Получение чата по идентификатору
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Чат</returns>
    Task<Chat> GetChatByIdAsync(Guid chatId);
    
    /// <summary>
    /// Обновление названия чата
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="chatNameUpdate">Модель обновления названия чата</param>
    /// <returns>Обновленный чат</returns>
    Task<Chat> UpdateChatNameAsync(Guid chatId, ChatNameUpdate chatNameUpdate);
    
    /// <summary>
    /// Добавление пользователя в чат
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task AddUserToChatAsync(Guid chatId, Guid userId);
    
    /// <summary>
    /// Удаление пользователя из чата
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task RemoveUserFromChatAsync(Guid chatId, Guid userId);
}