using System.Reflection;
using System.Text;
using System.Xml;

namespace vz.Extensions
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Converts an IEnumerable<T> to an XML string representation of a list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection to convert.</param>
        /// <param name="rootElementName">The name of the root XML element. Defaults to "Items".</param>
        /// <param name="itemElementName">The name for each item element. Defaults to the type name of T.</param>
        /// <returns>An XML string where each element from the source is represented as an XML element under a root element.</returns>
        public static string ToXml<T>(this IEnumerable<T> source, string rootElementName = "Items", string itemElementName = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartElement(rootElementName);

                foreach (var item in source)
                {
                    if (itemElementName == null)
                        itemElementName = typeof(T).Name;

                    writer.WriteStartElement(itemElementName);

                    foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        // Only serialize properties that can be read
                        if (property.CanRead)
                        {
                            var value = property.GetValue(item);
                            if (value != null)  // Avoid writing null values or properties that can't be read
                            {
                                writer.WriteElementString(property.Name, value.ToString());
                            }
                        }
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            return sb.ToString();
        }
    }
}
