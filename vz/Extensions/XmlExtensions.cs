// File: Extensions/XmlExtensions.cs
using System.Xml.Linq;

namespace vz.Extensions
{
    /// <summary>
    /// Provides XML serialization extensions for collections.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Converts an enumerable collection of objects into an XML string representation.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
        /// <param name="source">The source collection to convert to XML.</param>
        /// <returns>A string containing XML representation of the source collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an error occurs during the XML conversion process, wrapping the original exception.</exception>
        /// <remarks>
        /// - Each object in the collection is represented as an XML element named after the type or "Item" if the type name is null.
        /// - Properties of each object are converted into child elements of their respective object's element, with the property name as the element name.
        /// - If a property value is null, an empty string is used in the XML.
        /// - Errors encountered when accessing property values are captured as text within the XML element.
        /// - Uses <see cref="System.Xml.Linq"/> for XML manipulation.
        /// </remarks>
        public static string ToXml<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The source collection cannot be null.");
            }

            try
            {
                XElement root = new XElement("root");

                foreach (T? item in source)
                {
                    XElement element = new XElement(typeof(T).Name ?? "Item");

                    if (item != null)
                    {
                        foreach (System.Reflection.PropertyInfo prop in typeof(T).GetProperties())
                        {
                            try
                            {
                                object? value = prop.GetValue(item);

                                // Add the element even if it's null, but content will be empty string
                                element.Add(new XElement(prop.Name, value?.ToString() ?? string.Empty));
                            }
                            catch (Exception ex)
                            {
                                element.Add(new XElement(prop.Name, $"Error accessing property: {ex.Message}"));
                            }
                        }
                    }
                    else
                    {
                        element.Add(string.Empty);
                    }

                    root.Add(element);
                }

                return root.ToString(SaveOptions.OmitDuplicateNamespaces);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while converting the collection to XML: {ex.Message}", ex);
            }
        }
    }
}