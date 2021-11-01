using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Info about sea-surface temperature and state
    /// </summary>
    public class SeaCondition
    {
        /// <summary>
        /// Temperature in Celsius
        /// </summary>
        public int SeaTemperature { get; init; }

        /// <summary>
        /// Average height of the waves
        /// </summary>
        public int WaveHeight { get; init; }

        /// <summary>
        /// Sea state
        /// </summary>
        public SeaStateType SeaState { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public SeaCondition() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal SeaCondition(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of prevailing visibility tokens is empty");
                return;
            }

            var firstToken = tokens.First();
            var splittedToken = firstToken.Split("/");

            SeaTemperature = getSeaTemperature(splittedToken[0]);

            var stateToken = splittedToken[1];
            if (stateToken.Contains("H"))
            {
                WaveHeight = int.Parse(stateToken[1..]);
                SeaState = SeaStateType.None;
                return;
            }

            SeaState = stateToken.Contains("/")
                ? SeaStateType.None
                : (SeaStateType)int.Parse(stateToken[1..]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get sea surface temperature
        /// </summary>
        /// <param name="temperatureString">Temperature string</param>
        /// <returns></returns>
        private int getSeaTemperature(string temperatureString)
        {
            temperatureString = temperatureString.Replace("W", "");

            if (!temperatureString.StartsWith("M"))
                return int.Parse(temperatureString);

            temperatureString = temperatureString.Replace("M", "");

            return int.Parse(temperatureString) * -1;
        }

        #endregion
    }
}
