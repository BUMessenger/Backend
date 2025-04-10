using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserFiltersDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [FromQuery(Name = "name")]
    public string? Name { get; set; } = null;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [FromQuery(Name = "surname")] 
    public string? Surname { get; set; } = null;
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [FromQuery(Name = "fathername")] 
    public string? Fathername { get; set; } = null;
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    [FromQuery(Name = "email")] 
    public string? Email { get; set; } = null;
    
    /// <summary>
    /// Идентификатор чата пользователя
    /// </summary>
    [FromQuery(Name = "chatId")] 
    public Guid? ChatId { get; set; } = null;

    public UserFiltersDto(string? name, 
        string? surname, 
        string? fathername, 
        string? email, 
        Guid? chatId)
    {
        Name = name;
        Surname = surname;
        Fathername = fathername;
        Email = email;
        ChatId = chatId;
    }

    public UserFiltersDto()
    {
    }
}