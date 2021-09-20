using System.Collections.Generic;
using MetarParserCore.Enums;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Date and time of the airport by Zulu
    /// </summary>
    public class ObservationDayTime
    {
        /// <summary>
        /// Current month
        /// </summary>
        public Month Month { get; init; }

        /// <summary>
        /// Day of the current month
        /// </summary>
        public int Day { get; init; }

        /// <summary>
        /// Hours
        /// </summary>
        public int Hours { get; init; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public ObservationDayTime() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <param name="month">Current month</param>
        public ObservationDayTime(string[] tokens, List<string> errors, Month month)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of observation day time tokens is empty");
                return;
            }

            Month = month;
            Day = int.Parse(tokens[0].Substring(0, 2));
            Hours = int.Parse(tokens[0].Substring(2, 2));
            Minutes = int.Parse(tokens[0].Substring(4, 2));
        }

        #endregion
    }
}
