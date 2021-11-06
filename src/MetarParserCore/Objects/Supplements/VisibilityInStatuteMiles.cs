using System.Text.RegularExpressions;
using MetarParserCore.Common;

namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// Prevailing visibility measuring in statute miles
    /// </summary>
    public class VisibilityInStatuteMiles
    {
        /// <summary>
        /// (M sign) - denotes less than represented value
        /// </summary>
        public bool LessThanSign { get; init; }

        /// <summary>
        /// Whole number miles of visibility/whole number of mixed fraction 
        /// </summary>
        public int WholeNumber { get; init; }

        /// <summary>
        /// Numerator of the fraction
        /// </summary>
        public int Numerator { get; init; }

        /// <summary>
        /// Denominator of the fraction
        /// </summary>
        public int Denominator { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public VisibilityInStatuteMiles() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        internal VisibilityInStatuteMiles(string[] tokens)
        {
            foreach (var token in tokens)
            {
                var current = token;
                if (Regex.IsMatch(current, ParseRegex.VisibilityWholeNumber))
                {
                    WholeNumber = int.Parse(current);
                    continue;
                }

                current = token.Replace("SM", "");
                if (current.StartsWith("M"))
                {
                    LessThanSign = true;
                    current = current.Replace("M", "");
                }

                var fraction = current.Split("/");
                if (fraction.Length == 1)
                {
                    WholeNumber = int.Parse(fraction[0]);
                    continue;
                }

                Numerator = int.Parse(fraction[0]);
                Denominator = int.Parse(fraction[1]);
            }
        }

        #endregion
    }
}
