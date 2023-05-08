using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MetarParserCore.Objects.Supplements;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Represented forecast period for TAF reports
    /// </summary>
    [DataContract(Name = "tafForecastPeriod")]
    public class TafForecastPeriod
    {
        /// <summary>
        /// Start of the forecast period
        /// </summary>
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public TafDayTime Start { get; init; }

        /// <summary>
        /// End of the forecast period
        /// </summary>
        [DataMember(Name = "end", EmitDefaultValue = false)]
        public TafDayTime End { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public TafForecastPeriod() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal TafForecastPeriod(string[] tokens, List<string> errors)
        {
            const int startIdx = 0;
            const int endIdx = 1;

            if (tokens.Length == 0)
            {
                errors.Add("TAF forecast token is not found");
                return;
            }

            var parts = tokens.First().Split("/");

            Start = GetTafDayTime(parts[startIdx]);
            End = GetTafDayTime(parts[endIdx]);
        }

        #endregion

        #region Private methods

        private TafDayTime GetTafDayTime(string token) => new()
        {
            Day = int.Parse(token[..2]),
            Hours = int.Parse(token[2..])
        };

        #endregion
    }
}
