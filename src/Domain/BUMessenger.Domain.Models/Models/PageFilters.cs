using System;

#nullable enable
namespace BUMessenger.Domain.Models.Models
{
    /// <summary>
    /// Фильтры постраничного получения элементов коллекции.
    /// </summary>
    public record PageFilters
    {
        /// <summary>
        /// Число элементов на получаемой странице.
        /// </summary>
        public required int Limit { get; init; }

        /// <summary>
        /// С какого элемента начинать отсчет элементов на странице.
        /// </summary>
        public required int Skip { get; init; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PageFilters"/>.
        /// </summary>
        /// <param name="limit">Число элементов на странице (должно быть больше 0).</param>
        /// <param name="skip">Смещение, с какого элемента начинать (должно быть неотрицательным).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если <paramref name="limit"/> меньше 1 или <paramref name="skip"/> меньше 0.
        /// </exception>
        public PageFilters(int limit, int skip)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be greater than 0.");
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip), "Skip must be non-negative.");

            Limit = limit;
            Skip = skip;
        }
    }
}