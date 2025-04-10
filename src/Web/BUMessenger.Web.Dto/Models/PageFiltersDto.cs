using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace BUMessenger.Web.Dto.Models;

public class PageFiltersDto
{
    /// <summary>
    /// Количество пропускаемых записей
    /// </summary>
    [FromQuery(Name = "skip")]
    public int Skip { get; set; } = 0;

    /// <summary>
    /// Количество взятых записей
    /// </summary>
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 100;

    public PageFiltersDto(int skip, int limit)
    {
        Skip = skip;
        Limit = limit;
    }

    public PageFiltersDto()
    {
    }
}