using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="userCreate">Начальные данные о пользователе для создания</param>
    /// <returns>Новый пользователь</returns>
    Task<User> AddUserAsync(UserCreateWithPassword userCreate);
    
    /// <summary>
    /// True -- если пользователь с таким email существует, false -- иначе
    /// </summary>
    /// <param name="email">Email пользователя</param>
    /// <returns>Флаг существования пользователя</returns>
    Task<bool> IsUserExistByEmailAsync(string email);
    
    /// <summary>
    /// Получение пользователя по email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns>Пользователь, если найден, null -- иначе</returns>
    Task<User?> FindUserByEmailAsync(string email);
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Пользователь, если найден, null -- иначе</returns>
    Task<User?> FindUserByIdAsync(Guid id);
    
    /// <summary>
    /// Проверяет совпадение переданного пароля и пароля пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="password">Проверяемый пароль</param>
    /// <returns>True -- пароли совпадают, false -- иначе</returns>
    Task<bool> IsPasswordMatchAsync(Guid userId, string password);
    
    /// <summary>
    /// Получает список пользователей по фильтрам
    /// </summary>
    /// <param name="userFilters">Фильтры пользователей</param>
    /// <param name="pageFilters">Параметры пагинации</param>
    /// <returns>Список пользователей</returns>
    Task<Users> GetUsersByFiltersAsync(UserFilters userFilters, PageFilters pageFilters);
    
    /// <summary>
    /// Обновляет пароль пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="passwordHashed">Новый пароль в захешированном виде</param>
    /// <returns></returns>
    Task UpdatePasswordByIdAsync(Guid id, string passwordHashed);
}