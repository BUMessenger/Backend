namespace BUMessenger.DataAccess.Models.Models;

public class ChatDb
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название чата
    /// </summary>
    public string ChatName { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей сообщений
    /// </summary>
    public List<MessageDb> Messages { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей информации о чатах и пользователях
    /// </summary>
    public List<ChatUserInfoDb> ChatUserInfos { get; set; }

    public ChatDb(Guid id, string chatName)
    {
        Id = id;
        ChatName = chatName;
    }
}