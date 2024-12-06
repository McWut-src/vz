using System.Globalization;
using System.Reflection;
using System.Text;

namespace vz.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> to a JSON string representation of an array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection to convert. Cannot be null.</param>
        /// <returns>A JSON string where each element from the source is an object or value in a JSON array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the source collection is null.</exception>
        /// <remarks>
        /// - Strings are properly escaped for JSON compatibility.
        /// - Booleans are converted to lowercase.
        /// - Floating-point numbers are formatted to avoid scientific notation while retaining precision.
        /// - For complex objects, all public properties are serialized.
        /// </remarks>
        public static string ToJson<T>(this IEnumerable<T> source)
        {
            // Check if the source is null, throw an exception if it is
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source collection cannot be null.");

            // Start building the JSON string with an opening square bracket
            StringBuilder json = new("[");
            bool first = true;

            // Iterate over each item in the source collection
            foreach (T item in source)
            {
                string itemJson;

                // Determine the JSON representation of the item
                if (item == null)
                {
                    itemJson = "null";  // Represent null items as 'null' in JSON
                }
                else if (item is string)
                {
                    // Escape the string for JSON to handle special characters
                    itemJson = $"\"{item.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
                }
                else if (item is IConvertible)
                {
                    // Convert to string representation, handle special cases for JSON format
                    if (item is bool)
                    {
                        itemJson = item.ToString().ToLower();  // JSON uses lowercase for booleans
                    }
                    else if (item is float or double)
                    {
                        // Use 'G17' format specifier to avoid scientific notation for better precision
                        itemJson = ((IFormattable)item).ToString("G17", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        itemJson = item.ToString();  // For other convertible types
                    }
                }
                else
                {
                    // For non-primitive types, serialize all public properties
                    itemJson = SerializeObject(item);  // Custom serialization for complex objects
                }

                // Append the item's JSON to our main JSON string
                if (!first) json.Append(",");  // Add comma separator for subsequent items
                json.Append(itemJson);
                first = false;
            }

            // Close the JSON array with a closing square bracket
            json.Append("]");

            // Return the final JSON string representation
            return json.ToString();
        }

        private static string SerializeObject(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // If there are no public properties, return an empty JSON object
            if (properties.Length == 0) return "{}";

            // Use LINQ to create a string for each property in JSON format
            var jsonProperties = properties.Select(prop =>
            {
                object value = prop.GetValue(obj);
                string? valueJson;  // Default string conversion
                if (value is string)
                {
                    valueJson = value == null ? "null" :  // Handle null property values
                                   (string?)$"\"{value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
                }
                else
                {
                    if (value is bool)
                    {
                        valueJson = value == null ? "null" :  // Handle null property values
                                   value.ToString().ToLower() as string;
                    }
                    else
                    {
                        valueJson = value == null ? "null" :  // Handle null property values
                                   value is float or double ? ((IFormattable)value).ToString("G17", CultureInfo.InvariantCulture) :  // Handle floats and doubles
                                   value.ToString();
                    }
                }

                return $"\"{prop.Name}\":{valueJson}";
            });

            // Join all properties into a JSON object string
            return "{" + string.Join(",", jsonProperties) + "}";
        }
    }

}