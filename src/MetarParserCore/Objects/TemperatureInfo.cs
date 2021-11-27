﻿using System.Collections.Generic;
using System.Linq;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Information about air temperature and dew point
    /// </summary>
    public class TemperatureInfo
    {
        /// <summary>
        /// Temperature value in Celsius
        /// </summary>
        public int Value { get; init; }

        /// <summary>
        /// Temperature dew point in Celsius
        /// </summary>
        public int DewPoint { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public TemperatureInfo() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        /// <param name="tokens">Temperature token</param>
        /// <param name="errors">Errors list</param>
        internal TemperatureInfo(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array with temperature token is empty");
                return;
            }

            var temperatureToken = tokens.First();
            if (string.IsNullOrEmpty(temperatureToken))
            {
                errors.Add("Cannot parse empty temperature token");
                return;
            }

            var values = temperatureToken.Split('/');
            if (values.Length < 2 || values.Length >= 2 && string.IsNullOrEmpty(values[1]))
            {
                errors.Add($"Cannot parse \"{temperatureToken}\" as temperature token");
                return;
            }

            Value = getTemperatureValue(values[0]);
            DewPoint = getTemperatureValue(values[1]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Convert temperature value to integer considering sign "M"
        /// </summary>
        /// <param name="stringValue">Temperature value</param>
        /// <returns></returns>

        private int getTemperatureValue(string stringValue)
        {
            if (stringValue.Contains("M"))
                stringValue = stringValue.Replace("M", "-");

            return int.Parse(stringValue);
        }

        #endregion
    }
}
