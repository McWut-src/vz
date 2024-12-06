using System.Reflection;
using System.Text.Json;

namespace vz.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts an IEnumerable<T> to a JSON string representation of an array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection to convert.</param>
        /// <param name="includeHeader">Determines whether to include property names in the JSON objects. Defaults to true.</param>
        /// <returns>A JSON string where each element from the source is an object in a JSON array.</returns>
        public static string ToJson<T>(this IEnumerable<T> source, bool includeHeader = true)
        {
            ArgumentNullException.ThrowIfNull(source);

            var jsonArray = source.Select(item =>
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                          .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

                if (!includeHeader)
                {
                    // If headers are not included, we will just serialize the values in order without property names
                    return JsonSerializer.Serialize(properties.Select(p => p.GetValue(item)));
                }
                else
                {
                    // Create a dictionary to represent the object with property names as keys
                    var dict = properties.ToDictionary(p => p.Name, p => p.GetValue(item));
                    return JsonSerializer.Serialize(dict);
                }
            });

            // Serialize the entire collection as a JSON array
            return "[" + string.Join(",", jsonArray) + "]";
        }
    }
}
