namespace BUMessenger.DataAccess.Models.Models;

public class ChatUserInfoDb
{
    /// <summary>
    /// Идентификатор информационной записи о чате и пользователе
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор последнего прочитанного сообщения данного пользователя в данном чате
    /// </summary>
    public Guid LastReadMessageId { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей чатов
    /// </summary>
    public ChatDb Chat { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей пользователей
    /// </summary>
    public UserDb User { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей сообщений
    /// </summary>
    public MessageDb LastReadMessage { get; set; }

    public ChatUserInfoDb(Guid id, 
        Guid chatId, 
        Guid userId, 
        Guid lastReadMessageId)
    {
        Id = id;
        ChatId = chatId;
        UserId = userId;
        LastReadMessageId = lastReadMessageId;
    }
}