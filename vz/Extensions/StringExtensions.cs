using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace vz.Extensions
{

    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates the string to the specified maximum length, adding a suffix if necessary.
        /// </summary>
        /// <param name="value">The string to truncate.</param>
        /// <param name="maxLength">The maximum length of the resulting string.</param>
        /// <param name="truncationSuffix">The string to append at the end of the truncated string. Defaults to "...".</param>
        /// <returns>A truncated version of the string or the original if it's within the limit.</returns>
        public static string Truncate(this string value, int maxLength, string truncationSuffix = "...")
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (maxLength <= 0) return string.Empty;

            int strLength = value.Length;
            if (strLength <= maxLength) return value;

            return value.Substring(0, maxLength - truncationSuffix.Length) + truncationSuffix;
        }

        /// <summary>
        /// Converts a string to title case, where the first letter of each word is capitalized.
        /// </summary>
        /// <param name="value">The string to convert to title case.</param>
        /// <returns>The string in title case.</returns>
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Removes all whitespace characters from the string.
        /// </summary>
        /// <param name="value">The string to remove whitespace from.</param>
        /// <returns>A new string with all whitespace removed.</returns>
        public static string RemoveWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"\s+", "");
        }

        /// <summary>
        /// Removes extra spaces from the string, reducing multiple spaces to a single space.
        /// </summary>
        /// <param name="value">The string to clean up spaces in.</param>
        /// <returns>A new string with extra spaces removed.</returns>
        public static string RemoveExtraSpaces(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"\s+", " ");
        }

        /// <summary>
        /// Counts the occurrences of a substring within the string.
        /// </summary>
        /// <param name="value">The string to search within.</param>
        /// <param name="subString">The substring to count.</param>
        /// <returns>The number of times the substring occurs in the string.</returns>
        public static int CountOccurrences(this string value, string subString)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(subString)) return 0;
            int count = 0, i = 0;
            while ((i = value.IndexOf(subString, i, StringComparison.Ordinal)) != -1)
            {
                i += subString.Length;
                count++;
            }
            return count;
        }

        /// <summary>
        /// Capitalizes the first character of the string.
        /// </summary>
        /// <param name="value">The string to capitalize.</param>
        /// <returns>A new string with the first character capitalized.</returns>
        public static string CapitalizeFirst(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length > 1)
                return char.ToUpper(value[0]) + value.Substring(1);
            return value.ToUpper();
        }

        /// <summary>
        /// Removes diacritics (accents) from the string.
        /// </summary>
        /// <param name="value">The string to remove diacritics from.</param>
        /// <returns>A new string without diacritics.</returns>
        public static string RemoveDiacritics(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Checks if the string contains only letters and digits.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string contains only alphanumeric characters, false otherwise.</returns>
        public static bool IsAlphanumeric(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Repeats the string a specified number of times.
        /// </summary>
        /// <param name="value">The string to repeat.</param>
        /// <param name="count">The number of times to repeat the string.</param>
        /// <returns>A new string that is the concatenation of the input string repeated 'count' times.</returns>
        public static string Repeat(this string value, int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            return string.Concat(Enumerable.Repeat(value, count));
        }
    }
}