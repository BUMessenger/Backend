using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserNameUpdateDto
{
    /// <summary>
    /// Новое имя пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Новая фамилия пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("surname")]
    public string Surname { get; set; }
    
    /// <summary>
    /// Новое отчество пользователя
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("fathername")]
    public string? Fathername { get; set; }

    public UserNameUpdateDto(string name, 
        string surname, 
        string? fathername)
    {
        Name = name;
        Surname = surname;
        Fathername = fathername;
    }
}