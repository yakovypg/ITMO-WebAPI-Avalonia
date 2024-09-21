using System;
using System.Collections.Generic;

namespace StarWars.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static void Reset<T>(this ICollection<T> collection, IEnumerable<T>? newItems)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));

            collection.Clear();

            if (newItems is null)
                return;

            foreach (T item in newItems)
            {
                collection.Add(item);
            }
        }

        public static void Reset<T>(this ICollection<T> collection, params T[] newItems)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            Reset(collection, newItems);
        }
    }
}
