using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models;

public class PagedDto<T>
{
    /// <summary>
    /// Общее количество элементов, игнорируя пагинацию.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("count")]
    public int Count { get; init; }

    /// <summary>
    /// Список элементов после пагинации.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("items")]
    public List<T> Items { get; init; } = new List<T>();
    
    public PagedDto(int count, List<T> items)
    {
        Count = count;
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }
}