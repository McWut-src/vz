namespace vz.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides extension methods for file operations.
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Copies a file to a new location, optionally overwriting the destination file.
        /// </summary>
        /// <param name="fileInfo"> The file to copy. </param>
        /// <param name="destination"> The path of the new file to create. </param>
        /// <param name="overwrite"> If true, the destination file will be overwritten if it exists. </param>
        public static void CopyTo(this FileInfo fileInfo, string destination, bool overwrite = false)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            if (string.IsNullOrEmpty(destination))
                throw new ArgumentException("Destination path cannot be null or empty.", nameof(destination));

            fileInfo.CopyTo(destination, overwrite);
        }

        /// <summary>
        /// Moves a file to a new location, overwriting if the destination file exists.
        /// </summary>
        /// <param name="fileInfo"> The file to move. </param>
        /// <param name="destination"> The new file path for the file. </param>
        /// <param name="overwrite"> If true, the destination file will be overwritten if it exists. </param>
        public static void MoveTo(this FileInfo fileInfo, string destination, bool overwrite = false)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            if (string.IsNullOrEmpty(destination))
                throw new ArgumentException("Destination path cannot be null or empty.", nameof(destination));

            if (overwrite && File.Exists(destination))
                File.Delete(destination);

            fileInfo.MoveTo(destination);
        }

        /// <summary>
        /// Reads the contents of a file line by line, allowing for memory-efficient processing of large files.
        /// </summary>
        /// <param name="fileInfo"> The file to read from. </param>
        /// <param name="encoding"> The text encoding to use; if null, UTF-8 without BOM will be used. </param>
        /// <returns> An enumerable sequence of the file's lines. </returns>
        public static IEnumerable<string> ReadLines(this FileInfo fileInfo, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            using StreamReader reader = encoding == null
                ? new StreamReader(fileInfo.OpenRead())
                : new StreamReader(fileInfo.OpenRead(), encoding);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Asynchronously reads the contents of a file line by line.
        /// </summary>
        /// <param name="fileInfo"> The file to read from. </param>
        /// <param name="encoding"> The text encoding to use; if null, UTF-8 without BOM will be used. </param>
        /// <returns> An async enumerable sequence of the file's lines. </returns>
        public static async IAsyncEnumerable<string> ReadLinesAsync(this FileInfo fileInfo, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            using StreamReader reader = encoding == null
                ? new StreamReader(fileInfo.OpenRead())
                : new StreamReader(fileInfo.OpenRead(), encoding);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Writes lines to a file, overwriting the existing content if the file exists.
        /// </summary>
        /// <param name="fileInfo"> The file to write to. </param>
        /// <param name="lines"> The lines of text to write to the file. </param>
        /// <param name="encoding"> The encoding to use when writing the file. If null, UTF-8 without BOM will be used. </param>
        public static void WriteLines(this FileInfo fileInfo, IEnumerable<string> lines, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);
            ArgumentNullException.ThrowIfNull(lines);

            using StreamWriter writer = new StreamWriter(fileInfo.FullName, false, encoding ?? Encoding.UTF8);
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
        }

        /// <summary>
        /// Asynchronously writes lines to a file, overwriting the existing content if the file exists.
        /// </summary>
        /// <param name="fileInfo"> The file to write to. </param>
        /// <param name="lines"> An async enumerable sequence of lines to write to the file. </param>
        /// <param name="encoding"> The encoding to use when writing the file. If null, UTF-8 without BOM will be used. </param>
        public static async Task WriteLinesAsync(this FileInfo fileInfo, IAsyncEnumerable<string> lines, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);
            ArgumentNullException.ThrowIfNull(lines);

            using StreamWriter writer = new(fileInfo.FullName, false, encoding ?? Encoding.UTF8);
            await foreach (string line in lines)
            {
                await writer.WriteLineAsync(line);
            }
        }
    }
}