using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Web.Dto.Models;

namespace BUMessenger.Web.Dto.Converters;

public static class PageFiltersDtoConverter
{
    [return: NotNullIfNotNull(nameof(pageFilters))]
    public static PageFilters? ToDomain(this PageFiltersDto? pageFilters)
    {
        if (pageFilters is null)
            return null;

        return new PageFilters
        {
            Skip = pageFilters.Skip,
            Limit = pageFilters.Limit
        };
    }
}