namespace vz.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculates the first day (Monday) of the week for the given date.
        /// </summary>
        /// <param name="date"> The date for which to find the first day of the week. Can be null. </param>
        /// <returns> A DateTime representing the Monday of the week containing the input date, or DateTime.MinValue if date is null. </returns>
        public static DateTime FirstDayOfWeek(this DateTime? date)
        {
            // If the date is null, return DateTime.MinValue as a default
            if (!date.HasValue) return DateTime.MinValue;

            // Subtract the number of days from Monday (0) to the given day. This will move the date back to the start of the week (Monday).
            return date.Value.AddDays(-(int)date.Value.DayOfWeek + (int)DayOfWeek.Monday);
        }

        /// <summary>
        /// Calculates the first day (Monday) of the week for the given date.
        /// </summary>
        /// <param name="date"> The date for which to find the first day of the week. </param>
        /// <returns> A DateTime representing the Monday of the week containing the input date. </returns>
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            // Subtract the number of days from Monday (0) to the given day. This will move the date back to the start of the week (Monday).
            return date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
        }

        /// <summary>
        /// Determines if the given date falls on a weekday (Monday to Friday).
        /// </summary>
        /// <param name="date"> The date to check. Can be null. </param>
        /// <returns> True if the date is a weekday, false if it's a weekend day or if the date is null. </returns>
        public static bool IsWeekday(this DateTime? date)
        {
            // If the date is null, return false
            if (!date.HasValue) return false;

            // Check if the day of the week is neither Saturday nor Sunday
            return date.Value.DayOfWeek != DayOfWeek.Saturday && date.Value.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines if the given date falls on a weekday (Monday to Friday).
        /// </summary>
        /// <param name="date"> The date to check. </param>
        /// <returns> True if the date is a weekday, false if it's a weekend day. </returns>
        public static bool IsWeekday(this DateTime date)
        {
            // Check if the day of the week is neither Saturday nor Sunday
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Calculates the last day (Sunday) of the week for the given date.
        /// </summary>
        /// <param name="date"> The date for which to find the last day of the week. Can be null. </param>
        /// <returns> A DateTime representing the Sunday of the week containing the input date, or DateTime.MinValue if date is null. </returns>
        public static DateTime LastDayOfWeek(this DateTime? date)
        {
            // If the date is null, return DateTime.MinValue as a default
            if (!date.HasValue)
                return DateTime.MinValue;

            // First, find the Monday of the week, then add 6 days to get to Sunday
            return date.FirstDayOfWeek().AddDays(6);
        }

        /// <summary>
        /// Calculates the last day (Sunday) of the week for the given date.
        /// </summary>
        /// <param name="date"> The date for which to find the last day of the week. </param>
        /// <returns> A DateTime representing the Sunday of the week containing the input date. </returns>
        public static DateTime LastDayOfWeek(this DateTime date)
        {
            // First, find the Monday of the week, then add 6 days to get to Sunday
            return date.FirstDayOfWeek().AddDays(6);
        }

        /// Extends DateTime with a method to safely append a timestamp to a filename for Windows file systems. If no filename is provided, it returns
        /// a safe timestamp string that can be used as a filename. </summary> <param name="dateTime">The DateTime to convert.</param> <param
        /// name="fileName">The optional filename to which the timestamp will be appended. If empty, only the timestamp is returned.</param>
        /// <returns>A string representing either the filename with an appended timestamp or just the timestamp if fileName is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the filename provided is only whitespace.</exception>
        public static string ToWindowsFileName(this DateTime dateTime, string fileName = "")
        {
            // Check if the filename consists only of whitespace
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return dateTime.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            }

            // Check if the filename consists only of whitespace
            if (fileName.Trim() == string.Empty)
            {
                // Return a safe datetime string if no filename provided
                return dateTime.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            }

            // Extract the file name without extension
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            // Append the date and time, formatted to be Windows filename safe
            string timestamp = dateTime.ToString("yyyy-MM-dd_HH-mm-ss-fff");

            // Combine filename parts
            string newFileName = $"{nameWithoutExtension}_{timestamp}{extension}";

            // Ensure the filename length doesn't exceed Windows' max path length (260 characters minus a few for safety)
            if (newFileName.Length > 255)
            {
                // If too long, truncate the original filename, keeping the extension intact
                int charsToRemove = newFileName.Length - 255;
                nameWithoutExtension = nameWithoutExtension.Substring(0, nameWithoutExtension.Length - charsToRemove);
                newFileName = $"{nameWithoutExtension}_{timestamp}{extension}";
            }

            return newFileName;
        }
    }
}