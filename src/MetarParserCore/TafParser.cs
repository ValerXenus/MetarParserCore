using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;
using MetarParserCore.Interfaces;
using MetarParserCore.Objects;
using MetarParserCore.TokenLogic;

namespace MetarParserCore
{
    public class TafParser : IWeatherReportParser<Taf>
    {
        /// <summary>
        /// Current month
        /// </summary>
        private Month _currentMonth;

        public TafParser(Month currentMonth = Month.None)
        {
            _currentMonth = currentMonth;
        }

        #region Public methods

        /// <summary>
        /// Parse method
        /// </summary>
        /// <param name="raw">Raw TAF string</param>
        /// <returns>Parsed TAF object</returns>
        public Taf Parse(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                return new Taf
                {
                    ParseErrors = new[] { "Raw TAF is not correct" }
                };

            var rawTokens = raw.Clean().ToUpper().Split(" ");
            var groupedTokens = Recognizer.Instance().RecognizeAndGroupTokens(rawTokens);
            return new Taf(groupedTokens, _currentMonth);
        }

        /// <summary>
        /// Multiple parse TAFs method
        /// </summary>
        /// <param name="raws">Array of raw TAF strings</param>
        /// <returns>Array of parsed TAF objects</returns>
        public Taf[] Parse(string[] raws) =>
            raws.Select(Parse).ToArray();

        #endregion
    }
}
