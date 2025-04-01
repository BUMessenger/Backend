using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// Создаёт нового пользователя
    /// </summary>
    /// <param name="userCreate">Пользователь для добавления</param>
    /// <returns>Добавленный пользователь</returns>
    Task<User> AddUserAsync(UserCreate userCreate);
    
    /// <summary>
    /// Получение пользователя по email и паролю
    /// </summary>
    /// <param name="email">Адрес почты</param>
    /// <param name="password">Пароль</param>
    /// <returns>Пользователь</returns>
    Task<User> AuthUserByEmailPasswordAsync(string email, string password);
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Пользователь</returns>
    Task<User> GetUserByIdAsync(Guid id);
    
    /// <summary>
    /// Получает список пользователей по фильтрам
    /// </summary>
    /// <param name="userFilters">Фильтры пользователей</param>
    /// <param name="pageFilters">Параметры пагинации</param>
    /// <returns>Список пользователей</returns>
    Task<Users> GetUsersByFiltersAsync(UserFilters userFilters, PageFilters pageFilters);
    
    /// <summary>
    /// Сбрасывает пароль пользователя, новый присылает на почту
    /// </summary>
    /// <param name="userPasswordRecovery">Данные для сброса пароля</param>
    /// <returns></returns>
    Task RecoveryUserPasswordAsync(UserPasswordRecovery userPasswordRecovery);
    
    /// <summary>
    /// Удаляет пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns></returns>
    Task DeleteUserByIdAsync(Guid id);
    
    /// <summary>
    /// Обновляет ФИО пользователя по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="userNameUpdate">Новое ФИО</param>
    /// <returns>Пользователь</returns>
    Task<User> UpdateUserNameByIdAsync(Guid id, UserNameUpdate userNameUpdate);
    
    /// <summary>
    /// Меняет пароль пользователя на новый, при условии, что правильно введён текущий
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="userPasswordUpdate">Текущий и новый пароли</param>
    /// <returns>Пользователь</returns>
    Task<User> UpdateUserPasswordByIdAsync(Guid id, UserPasswordUpdate userPasswordUpdate);
}