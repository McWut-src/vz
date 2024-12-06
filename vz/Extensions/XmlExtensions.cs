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
        /// Converts an IEnumerable<T> to an XML string representation.
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source IEnumerable to convert to XML.</param>
        /// <returns>A string containing the XML representation of the source.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when an element in the source cannot be converted to XML.</exception>
        /// </summary>
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
