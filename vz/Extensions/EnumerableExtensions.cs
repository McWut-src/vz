namespace vz.Extensions
{

    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides extension methods for working with enumerables.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the most frequently occurring item in the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The source sequence to analyze.</param>
        /// <returns>The item with the highest frequency, or default(T) if the source is empty.</returns>
        public static T Mode<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var groups = source.GroupBy(i => i);
            return groups.OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).FirstOrDefault();
        }

        /// <summary>
        /// Calculates the median of a sequence of double values.
        /// </summary>
        /// <param name="source">The sequence of numbers.</param>
        /// <returns>The median value or throws an exception if the source is null or empty.</returns>
        public static double Median(this IEnumerable<double> source)
        {
            if (source == null || !source.Any())
                throw new ArgumentException("Source cannot be null or empty");

            var sorted = source.OrderBy(n => n).ToList();
            int count = sorted.Count;
            if (count % 2 == 0)
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2;
            else
                return sorted[count / 2];
        }

        /// <summary>
        /// Divides the source sequence into chunks of the specified size.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to chunk.</param>
        /// <param name="chunkSize">The size of each chunk.</param>
        /// <returns>An enumerable of chunks, where each chunk is an IEnumerable of T.</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            ArgumentNullException.ThrowIfNull(source);
            if (chunkSize <= 0) throw new ArgumentException("Chunk size must be positive", nameof(chunkSize));

            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of key to identify distinct elements by.</typeparam>
        /// <param name="source">The sequence to remove duplicates from.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>An IEnumerable<TSource> that contains distinct elements from the source sequence.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelector);

            return source.GroupBy(keySelector).Select(g => g.First());
        }

        /// <summary>
        /// Performs an action on each element of the sequence, but does not change the sequence itself.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to invoke an action on.</param>
        /// <param name="action">An action to apply to each element.</param>
        /// <returns>The original sequence, unaffected by the action.</returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(action);

            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }

        /// <summary>
        /// Flattens a sequence containing sequences into one sequence of elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequences.</typeparam>
        /// <param name="source">A sequence of sequences to flatten.</param>
        /// <returns>A single flattened sequence of all elements.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.SelectMany(x => x);
        }

        /// <summary>
        /// Removes all elements from the sequence that match the values provided.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to remove elements from.</param>
        /// <param name="values">Elements to remove from the sequence.</param>
        /// <returns>A new sequence with the specified values removed.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] values)
        {
            return source.Except(values.AsEnumerable());
        }

        /// <summary>
        /// Returns an enumerable that contains the current element and all subsequent elements from the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An IEnumerable<T> to return elements from.</param>
        /// <returns>An IEnumerable<T> that contains the current element and all subsequent elements from the source sequence.</returns>
        public static IEnumerable<T> SkipUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            bool found = false;
            foreach (T item in source)
            {
                if (!found && !predicate(item))
                {
                    continue;
                }
                found = true;
                yield return item;
            }
        }
    }
}
