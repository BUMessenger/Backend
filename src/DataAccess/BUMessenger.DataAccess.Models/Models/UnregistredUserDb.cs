namespace BUMessenger.DataAccess.Models.Models;

public class UnregisteredUserDb
{
    /// <summary>
    /// Идентификатор незарегистрированного пользователя
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Код подтверждения электронной почты
    /// </summary>
    public string ApproveCode { get; set; }
    
    /// <summary>
    /// Время когда истекает срок действия кода подтверждения
    /// </summary>
    public DateTime ExpiresAtUtc { get; set; }

    public UnregisteredUserDb(Guid id, 
        string email, 
        string approveCode, 
        DateTime expiresAtUtc)
    {
        Id = id;
        Email = email;
        ApproveCode = approveCode;
        ExpiresAtUtc = expiresAtUtc;
    }
}