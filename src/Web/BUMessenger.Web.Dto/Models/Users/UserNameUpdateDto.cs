using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UserNameUpdateDto
{
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Fathername { get; set; }

    public UserNameUpdateDto(string name, 
        string surname, 
        string fathername)
    {
        Name = name;
        Surname = surname;
        Fathername = fathername;
    }
}