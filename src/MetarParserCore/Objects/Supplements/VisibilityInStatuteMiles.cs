﻿using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using MetarParserCore.Common;

namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// Prevailing visibility measuring in statute miles
    /// </summary>
    [DataContract(Name = "visibilityInStatuteMiles")]
    public class VisibilityInStatuteMiles
    {
        /// <summary>
        /// (M sign) - denotes less than represented value
        /// </summary>
        [DataMember(Name = "lessThanSign", EmitDefaultValue = false)]
        public bool LessThanSign { get; init; }

        /// <summary>
        /// (P sign) - denotes greater than represented value
        /// </summary>
        [DataMember(Name = "greaterThanSign", EmitDefaultValue = false)]
        public bool GreaterThanSign { get; set; }

        /// <summary>
        /// Whole number miles of visibility/whole number of mixed fraction 
        /// </summary>
        [DataMember(Name = "wholeNumber", EmitDefaultValue = false)]
        public int WholeNumber { get; init; }

        /// <summary>
        /// Numerator of the fraction
        /// </summary>
        [DataMember(Name = "numerator", EmitDefaultValue = false)]
        public int Numerator { get; init; }

        /// <summary>
        /// Denominator of the fraction
        /// </summary>
        [DataMember(Name = "denominator", EmitDefaultValue = false)]
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
            const string statuteMilesToken = "SM";
            const string lessThanToken = "M";
            const string greaterThanToken = "P";

            foreach (var token in tokens)
            {
                var current = token;
                if (Regex.IsMatch(current, ParseRegex.VisibilityWholeNumber))
                {
                    WholeNumber = int.Parse(current);
                    continue;
                }

                current = token.Replace(statuteMilesToken, "");
                if (current.StartsWith(lessThanToken))
                {
                    LessThanSign = true;
                    current = current.Replace(lessThanToken, "");
                }

                if (current.StartsWith(greaterThanToken))
                {
                    GreaterThanSign = true;
                    current = current.Replace(greaterThanToken, "");
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
