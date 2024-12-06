using System.Reflection;

namespace vz.Extensions
{
    public static class CsvExtensions
    {
        /// <summary>
        /// Converts an IEnumerable<T> to an IEnumerable<string> where each string is a line in CSV format.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection to convert.</param>
        /// <param name="includeHeader">Determines whether to include the header row. Defaults to true.</param>
        /// <returns>An IEnumerable<string> where each string represents a line in CSV format.</returns>
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
