using System.Text.Json.Serialization;

namespace BUMessenger.Web.Dto.Models;

public class PageFiltersDto
{
    /// <summary>
    /// Количество пропускаемых записей
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("skip")]
    public int Skip { get; set; }
    
    /// <summary>
    /// Количество взятых записей
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    public PageFiltersDto(int skip, int limit)
    {
        Skip = skip;
        Limit = limit;
    }
}