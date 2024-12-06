namespace vz.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if an enum value is in the specified set of values.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="values">A set of enum values to check against.</param>
        /// <returns>True if the value is in the set, otherwise false.</returns>
        /// <exception cref="ArgumentException">Thrown if T is not an enum type.</exception>
        public static bool In<T>(this T value, params T[] values) where T : Enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enum type");

            return values.Contains(value);
        }
    }
}