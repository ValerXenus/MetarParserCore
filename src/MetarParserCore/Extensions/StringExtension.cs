using System.Text.RegularExpressions;

namespace MetarParserCore.Extensions
{
    /// <summary>
    /// Extension methods for standard string
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// Remove all redundant elements in current string
        /// </summary>
        /// <param name="current">Current string</param>
        /// <returns>Clean and ready to process string</returns>
        public static string Clean(this string current) => current
            .CleanMultipleSpaces()
            .Replace("\n", "")
            .Replace("\r", "")
            .Replace("\t", "")
            .Replace("=", "");

        /// <summary>
        /// Remove all redundant whitespaces
        /// </summary>
        /// <param name="current">Current string</param>
        /// <returns>String with single spaces</returns>
        public static string CleanMultipleSpaces(this string current) => 
            Regex.Replace(current, @"\s+", " ");
    }
}
