namespace BUMessenger.DataAccess.Models.Models;

public class MessageDb
{
    /// <summary>
    /// Идентификатор сообщений
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid ChatId { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя создавшего сообщение
    /// </summary>
    public Guid? CreatorId { get; set; }
    
    /// <summary>
    /// Идентификатор родительского сообщений
    /// </summary>
    public Guid? ParentMessageId { get; set; }
    
    /// <summary>
    /// Время отправки сообщения
    /// </summary>
    public DateTime SentAtUtc { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string MessageText { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей чатов
    /// </summary>
    public ChatDb Chat { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей пользователей
    /// </summary>
    public UserDb? Creator { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с родительским сообщением
    /// </summary>
    public MessageDb? ParentMessage { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с дочерними сообщениями
    /// </summary>
    public List<MessageDb> ChildMessages { get; set; } = [];

    /// <summary>
    /// Навигационное свойство для связи с таблицей информации о чатах и пользователях
    /// </summary>
    public List<ChatUserInfoDb> ChatUserInfos { get; set; } = [];

    public MessageDb(Guid id, 
        Guid chatId, 
        Guid? creatorId, 
        Guid? parentMessageId, 
        DateTime sentAtUtc, 
        string messageText)
    {
        Id = id;
        ChatId = chatId;
        CreatorId = creatorId;
        ParentMessageId = parentMessageId;
        SentAtUtc = sentAtUtc;
        MessageText = messageText;
    }
}