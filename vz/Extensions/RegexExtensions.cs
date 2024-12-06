namespace vz.Extensions
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides extension methods for string operations using regular expressions.
    /// </summary>
    public static class RegexExtensions
    {
        /// <summary>
        /// Attempts to match the input string against the pattern and returns the matched string or a default value if no match is found.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="defaultValue">The default value to return if no match is found. Defaults to an empty string.</param>
        /// <returns>The matched string if found, otherwise the default value.</returns>
        public static string MatchOrDefault(this string input, string pattern, string defaultValue = "")
        {
            if (input == null) return defaultValue; // Handle null input

            Match match = Regex.Match(input, pattern);
            return match.Success ? match.Value : defaultValue;
        }

        /// <summary>
        /// Replaces occurrences of a pattern in the input string with a replacement string or removes the pattern if no replacement is specified.
        /// </summary>
        /// <param name="input">The string to modify.</param>
        /// <param name="pattern">The regular expression pattern to look for.</param>
        /// <param name="replacement">The string to replace the pattern with. If null, the pattern is removed.</param>
        /// <returns>A new string with the replacements made.</returns>
        public static string? ReplaceOrRemove(this string input, string pattern, string? replacement = null)
        {
            if (input == null) return null; // Preserve null input

            return replacement == null
                ? Regex.Replace(input, pattern, string.Empty)
                : Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// Checks if the string matches the given regular expression pattern.
        /// </summary>
        /// <param name="input">The string to test.</param>
        /// <param name="pattern">The regular expression pattern to match against.</param>
        /// <returns>True if the entire string matches the pattern, false otherwise.</returns>
        public static bool IsMatch(this string input, string pattern)
        {
            if (input == null) return false; // Avoid null reference exceptions

            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Extracts all matches of the given pattern from the input string.
        /// </summary>
        /// <param name="input">The string to search for matches.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>An array of all matched strings.</returns>
        public static string[] ExtractMatches(this string input, string pattern)
        {
            if (string.IsNullOrEmpty(input)) return []; // Return empty array for null or empty input

            return Regex.Matches(input, pattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToArray();
        }

        /// <summary>
        /// Removes all matches of the given pattern from the string.
        /// </summary>
        /// <param name="input">The string to clean.</param>
        /// <param name="pattern">The regular expression pattern to remove.</param>
        /// <returns>A new string with all instances of the pattern removed.</returns>
        public static string? RemoveMatches(this string input, string pattern)
        {
            if (input == null) return null; // Preserve null input

            return Regex.Replace(input, pattern, string.Empty);
        }

        /// <summary>
        /// Splits the string into an array using the given pattern as the delimiter.
        /// </summary>
        /// <param name="input">The string to split.</param>
        /// <param name="pattern">The regular expression pattern used as the delimiter.</param>
        /// <returns>An array of strings resulting from the split operation.</returns>
        public static string[] SplitOnRegex(this string input, string pattern)
        {
            if (input == null) return new string[0]; // Handle null input

            return Regex.Split(input, pattern);
        }
    }
}
