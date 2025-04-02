using System;
using System.Collections.Generic;

#nullable enable
namespace BUMessenger.Domain.Models.Models
{
    /// <summary>
    /// Представление пагинированного списка.
    /// </summary>
    /// <typeparam name="T">Тип элемента в списке.</typeparam>
    public record Paged<T>
    {
        /// <summary>
        /// Общее количество элементов, игнорируя пагинацию.
        /// </summary>
        public required int Count { get; init; }

        /// <summary>
        /// Список элементов после пагинации.
        /// </summary>
        public required List<T> Items { get; init; } = new List<T>();

        /// <summary>
        /// Инициализация <see cref="Paged{T}"/> record.
        /// </summary>
        /// <param name="count">Общее количество элементов.</param>
        /// <param name="items">Список элементов после пагинации.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
        public Paged(int count, List<T> items)
        {
            Count = count;
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }
    }
}