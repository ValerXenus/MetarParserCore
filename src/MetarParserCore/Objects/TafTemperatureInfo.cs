using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MetarParserCore.Objects.Supplements;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// TAF forecast temperature info
    /// </summary>
    [DataContract(Name = "tafTemperatureInfo")]
    public class TafTemperatureInfo
    {
        /// <summary>
        /// Max expected temperature value
        /// </summary>
        [DataMember(Name = "maxForecastValue", EmitDefaultValue = false)]
        public TafTemperatureValue MaxForecastValue { get; init; }

        /// <summary>
        /// Min expected temperature value
        /// </summary>
        [DataMember(Name = "minForecastValue", EmitDefaultValue = false)]
        public TafTemperatureValue MinForecastValue { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public TafTemperatureInfo() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        /// <param name="tokens">Temperature tokens</param>
        /// <param name="errors">Errors list</param>
        internal TafTemperatureInfo(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array with forecast temperature tokens is empty");
                return;
            }

            foreach (var token in tokens)
            {
                if (string.IsNullOrEmpty(token))
                {
                    errors.Add("Cannot parse empty forecast temperature token");
                    continue;
                }

                try
                {
                    if (token.StartsWith(MaxTemperatureToken))
                        MaxForecastValue = GetTemperatureValue(token);
                    else
                        MinForecastValue = GetTemperatureValue(token);
                }
                catch (Exception exception)
                {
                    errors.Add(exception.Message);
                    break;
                }
            }
        }

        #endregion

        #region Private constants

        private const string MaxTemperatureToken = "TX";

        private const string MinTemperatureToken = "TN";

        #endregion

        #region Private methods

        /// <summary>
        /// Parse string temperature token
        /// </summary>
        /// <param name="token">Temperature token</param>
        /// <returns></returns>
        private TafTemperatureValue GetTemperatureValue(string token)
        {
            token = token.Replace(MaxTemperatureToken, "")
                .Replace(MinTemperatureToken, "")
                .Replace("Z", "");

            if (token.Contains("M"))
                token = token.Replace("M", "-");

            var parts = token.Split("/");
            if (parts.Length < 2 || string.IsNullOrEmpty(parts[1]))
                throw new Exception($"Cannot parse \"{token}\" as forecast temperature token");

            return new TafTemperatureValue
            {
                Value = int.Parse(parts[0]),
                Day = int.Parse(parts[1][..2]),
                Hours = int.Parse(parts[1][2..])
            };
        }

        #endregion
    }
}
