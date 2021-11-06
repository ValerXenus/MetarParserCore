using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Special weather conditions
    /// </summary>
    public class WeatherPhenomena
    {
        /// <summary>
        /// Ordered array of weather conditions
        /// </summary>
        public WeatherCondition[] WeatherConditions { get; set; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public WeatherPhenomena() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Weather token</param>
        /// <param name="errors">Parse errors list</param>
        internal WeatherPhenomena(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of present weather tokens is empty");
                return;
            }

            var parsedData = new List<WeatherCondition>();
            var weatherToken = tokens.First();

            if (weatherToken.Equals("NSW"))
            {
                WeatherConditions = new[] { WeatherCondition.NoSignificantWeather };
                return;
            }

            if (weatherToken.StartsWith("RE"))
                weatherToken = weatherToken.Replace("RE", "");

            if (weatherToken.StartsWith("-"))
            {
                parsedData.Add(WeatherCondition.Light);
                weatherToken = weatherToken[1..];
            }

            if (weatherToken.StartsWith("+"))
            {
                parsedData.Add(WeatherCondition.Heavy);
                weatherToken = weatherToken[1..];
            }

            var weatherCodes = splitIntoCodes(weatherToken);
            if (weatherCodes.Length == 0)
            {
                errors.Add($"Cannot parse weather token: \"{weatherToken}\"");
                return;
            }
            parsedData.AddRange(weatherCodes.Select(EnumTranslator.GetValueByDescription<WeatherCondition>));

            WeatherConditions = parsedData.ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Splits weather string into weather codes
        /// </summary>
        /// <param name="weatherToken">Weather token</param>
        /// <returns></returns>
        private string[] splitIntoCodes(string weatherToken)
        {
            var length = weatherToken.Length / 2;
            var outcome = new string[length];

            for (var i = 0; i < length; i++)
            {
                outcome[i] = weatherToken.Substring(i * 2, 2);
            }

            return outcome;
        }

        #endregion
    }
}
