using BUMessenger.Domain.Models.Models.UnregisteredUsers;

namespace BUMessenger.Domain.Interfaces.Repositories;

public interface IUnregisteredUserRepository
{
    /// <summary>
    /// Создаёт нового незарегистрированного пользователя
    /// </summary>
    /// <param name="unregisteredUser">Незарегистрированный пользователь для добавления</param>
    /// <returns>Добавленный незарегистрированный пользователь</returns>
    Task<UnregisteredUser> AddUnregisteredUserAsync(UnregisteredUserCreateWithAdditionalData unregisteredUser);
    
    /// <summary>
    /// Возвращает код подтверждения незарегистрированного пользователя по email
    /// </summary>
    /// <param name="email">Почта незарегистрированного пользователя</param>
    /// <returns>Код подтверждения</returns>
    Task<string?> FindUnregisteredUserApproveCodeByEmailAsync(string email);
    
    /// <summary>
    /// True -- если незарегистрированный пользователь с таким email существует, false -- иначе
    /// </summary>
    /// <param name="email">Почта незарегистрированного пользователя</param>
    /// <returns>Флаг существования</returns>
    Task<bool> IsUnregisteredUserExistByEmailAsync(string email);
    
    /// <summary>
    /// Получение незарегистрированного пользователя по email
    /// </summary>
    /// <param name="email">Почта незарегистрированного пользователя</param>
    /// <returns>Незарегистрированный пользователь</returns>
    Task<string?> FindUnregisteredUserPasswordByEmailAsync(string email);
}