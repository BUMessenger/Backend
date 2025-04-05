namespace BUMessenger.DataAccess.Models.Models;

public class UserDb
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? FatherName { get; set; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль пользователя в захешированном виде
    /// </summary>
    public string PasswordHashed { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей токенов
    /// </summary>
    public List<AuthTokenDb> AuthTokens { get; set; }

    /// <summary>
    /// Навигационное свойство для связи с таблицей сообщений
    /// </summary>
    public List<MessageDb> Messages { get; set; } = [];

    /// <summary>
    /// Навигационное свойство для связи с таблицей информации о чатах и пользователях
    /// </summary>
    public List<ChatUserInfoDb> ChatUserInfos { get; set; } = [];
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей чатов
    /// </summary>
    public List<ChatDb> Chats { get; set; } = [];

    public UserDb(Guid id, 
        string name, 
        string surname, 
        string? fatherName, 
        string email, 
        string passwordHashed)
    {
        Id = id;
        Name = name;
        Surname = surname;
        FatherName = fatherName;
        Email = email;
        PasswordHashed = passwordHashed;
    }
}