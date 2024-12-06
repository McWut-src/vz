using System.Reflection;

namespace vz.Extensions
{
    /// <summary>
    /// Provides extension methods for object mapping.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Maps the properties of the source object to a new instance of the destination type.
        /// Tries to map:
        /// 1. Properties with the same name and type (case-sensitive).
        /// 2. Properties with the same name and type (case-insensitive).
        /// 3. Properties with the same name (case-sensitive) where source can be converted to string.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDest">The type of the destination object.</typeparam>
        /// <param name="source">The source object to map from.</param>
        /// <returns>An instance of <typeparamref name="TDest"/> with properties mapped from the source.</returns>
        /// <exception cref="ArgumentNullException">Thrown if source is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if destination type cannot be instantiated.</exception>
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
