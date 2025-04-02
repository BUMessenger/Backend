using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models.Users;

public class UsersDto
{
    /// <summary>
    /// Количество элементов в выдаче без учета пагинации
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    /// <summary>
    /// Список пользователей
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("items")]
    public List<UserDto> Items { get; set; }

    public UsersDto(int count, List<UserDto> items)
    {
        Count = count;
        Items = items;
    }
}