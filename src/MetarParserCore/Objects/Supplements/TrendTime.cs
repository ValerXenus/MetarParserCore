﻿using System.Collections.Generic;

namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// Info about trend times
    /// </summary>
    public class TrendTime
    {
        /// <summary>
        /// Attribute "AT"
        /// </summary>
        public Time AtTime { get; init; }

        /// <summary>
        /// Attribute "FM"
        /// </summary>
        public Time FromTime { get; init; }

        /// <summary>
        /// Attribute "TL"
        /// </summary>
        public Time TillTime { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public TrendTime() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        internal TrendTime(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
                return;

            foreach (var token in tokens)
            {
                switch (token)
                {
                    case { } when token.StartsWith("AT"):
                        AtTime = GetTimeValue(token[2..]);
                        break;
                    case { } when token.StartsWith("FM"):
                        FromTime = GetTimeValue(token[2..]);
                        break;
                    case { } when token.StartsWith("TL"):
                        TillTime = GetTimeValue(token[2..]);
                        break;
                    default:
                        errors.Add($"Unexpected time token {token}");
                        return;
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Parse time token
        /// </summary>
        /// <param name="stringValue">Incoming string time</param>
        /// <returns></returns>
        private Time GetTimeValue(string stringValue)
        {
            return new Time(int.Parse(stringValue[..2]), int.Parse(stringValue[2..]));
        }

        #endregion
    }
}
