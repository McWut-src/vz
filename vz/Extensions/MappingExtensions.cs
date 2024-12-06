using System.Reflection;

namespace vz.Extensions
{
    /// <summary>
    /// Provides extension methods for object mapping.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps properties from a source object to a new destination object of a different type, using various mapping strategies.
        /// </summary>
        /// <typeparam name="TSource"> The type of the source object. Must be a class reference type. </typeparam>
        /// <typeparam name="TDest"> The type of the destination object. Must have a parameterless constructor. </typeparam>
        /// <param name="source"> The source object to map from. </param>
        /// <returns> A new instance of <typeparamref name="TDest" /> with mapped properties from the source. </returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="source" /> is null. </exception>
        /// <remarks>
        /// - The mapping process involves:
        /// 1. **Exact Match**: Maps properties with the same name and type (case-sensitive).
        /// 2. **Case-Insensitive Match**: Maps properties with the same name but different case and the same type.
        /// 3. **String Conversion**: Maps properties with the same name to string properties in the destination if not already set.
        /// 4. **Case-Insensitive String Conversion**: Similar to step 3 but case-insensitive for property names.
        /// - Only public instance properties are considered for mapping.
        /// - Properties are mapped in order, with later steps only mapping if the property hasn't been set by a previous step.
        /// </remarks>
        public static TDest MapTo<TSource, TDest>(this TSource source) where TDest : new() where TSource : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source object cannot be null.");

            TDest destination = new TDest();
            Type sourceType = typeof(TSource);
            Type destType = typeof(TDest);

            // Step 1: Map properties with the same name and type (case-sensitive)
            foreach (PropertyInfo destProp in destType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destProp.CanWrite) continue;

                PropertyInfo? sourceProp = sourceType.GetProperty(destProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp != null && sourceProp.CanRead && sourceProp.PropertyType == destProp.PropertyType)
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                }
            }

            // Step 2: Map properties with the same name and type (case-insensitive)
            foreach (PropertyInfo destProp in destType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destProp.CanWrite || destProp.GetValue(destination) != null) continue;

                PropertyInfo? sourceProp = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => p.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase) &&
                                         p.PropertyType == destProp.PropertyType);

                if (sourceProp != null && sourceProp.CanRead)
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                }
            }

            // Step 3: Map properties with the same name (case-sensitive) where source can be converted to string
            foreach (PropertyInfo destProp in destType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destProp.CanWrite || destProp.GetValue(destination) != null || destProp.PropertyType != typeof(string)) continue;

                PropertyInfo? sourceProp = sourceType.GetProperty(destProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp != null && sourceProp.CanRead)
                {
                    object? value = sourceProp.GetValue(source) ?? null;
                    destProp.SetValue(destination, value?.ToString());
                }
            }

            // Step 4: Map properties with the same name (case-insensitive) where source can be converted to string
            foreach (PropertyInfo destProp in destType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!destProp.CanWrite || destProp.GetValue(destination) != null || destProp.PropertyType != typeof(string)) continue;

                PropertyInfo? sourceProp = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => p.Name.Equals(destProp.Name, StringComparison.OrdinalIgnoreCase));

                if (sourceProp != null && sourceProp.CanRead)
                {
                    object? value = sourceProp.GetValue(source);
                    destProp.SetValue(destination, value?.ToString());
                }
            }

            return destination;
        }
    }
}