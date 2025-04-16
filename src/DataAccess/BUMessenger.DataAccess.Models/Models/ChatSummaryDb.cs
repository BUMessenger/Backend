namespace BUMessenger.DataAccess.Models.Models;

public class ChatSummaryDb
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
    /// Последнее сообщение
    /// </summary>
    public MessageDb? LastMessage { get; set; }
    
    /// <summary>
    /// Количество непрочитанных сообщений
    /// </summary>
    public int UnreadMessagesNumber { get; set; }
}