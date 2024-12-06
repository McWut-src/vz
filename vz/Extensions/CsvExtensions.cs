using System.Reflection;

namespace vz.Extensions
{
    public static class CsvExtensions
    {
        /// <summary>
        /// Converts an enumerable sequence of objects to a CSV-formatted string sequence.
        /// </summary>
        /// <typeparam name="T"> The type of the elements in the source sequence. </typeparam>
        /// <param name="source"> The source sequence to convert to CSV format. </param>
        /// <param name="includeHeader"> A boolean indicating whether to include a header row in the CSV output. Default is true. </param>
        /// <returns> An <see cref="IEnumerable{string}" /> where each element is a line of the CSV file. </returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="source" /> is null. </exception>
        /// <remarks>
        /// - This method uses reflection to get properties from objects of type T.
        /// - Property values are converted to strings, with special handling for commas and quotes to ensure proper CSV formatting.
        /// - The method yields strings one at a time, allowing for lazy evaluation of the sequence.
        /// - Empty or null values in the source objects will be represented as empty strings in the CSV.
        /// </remarks>
        public static IEnumerable<string> ToCsv<T>(this IEnumerable<T> source, bool includeHeader = true)
        {
            ArgumentNullException.ThrowIfNull(source);

            // Get the properties of T to use as headers
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Yield the header row if includeHeader is true
            if (includeHeader)
            {
                yield return string.Join(",", properties.Select(p => p.Name));
            }

            // Yield data rows
            foreach (T? item in source)
            {
                List<string> values = properties
                    .Select(p =>
                    {
                        string value = p.GetValue(item)?.ToString() ?? "";

                        // Escape commas and quotes
                        if (value.Contains(',') || value.Contains('"'))
                        {
                            value = '"' + value.Replace("\"", "\"\"") + '"';
                        }
                        return value;
                    })
                    .ToList();

                yield return string.Join(",", values);
            }
        }
    }
}