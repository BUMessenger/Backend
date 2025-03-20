using BUMessenger.Domain.Models.Models.UnregisteredUsers;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IUnregisteredUserService
{
    /// <summary>
    /// Создаёт нового незарегистрированного пользователя
    /// </summary>
    /// <param name="unregisteredUser">Незарегистрированный пользователь для добавления</param>
    /// <returns>Добавленный незарегистрированный пользователь</returns>
    Task<UnregisteredUser> AddUnregisteredUserAsync(UnregisteredUserCreate unregisteredUser);
}