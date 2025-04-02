using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("surname")]
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("fathername")]
    public string? Fathername { get; set; }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    public UserDto(Guid id, 
        string name, 
        string surname, 
        string? fathername, 
        string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Fathername = fathername;
        Email = email;
    }
}