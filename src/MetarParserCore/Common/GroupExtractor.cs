using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.TokenLogic;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Extensions;

namespace MetarParserCore.Common
{
    /// <summary>
    /// Methods to extract some groups from dictionary
    /// </summary>
    internal static class GroupExtractor
    {
        /// <summary>
        /// Get airport ICAO code
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        public static string GetAirportIcao(Dictionary<TokenType, string[]> groupedTokens, List<string> errors)
        {
            var airportValue = groupedTokens.GetTokenGroupOrDefault(TokenType.Airport);
            if (airportValue.Length > 0)
                return airportValue[0];

            errors.Add("Airport ICAO code not found");
            return null;
        }

        /// <summary>
        /// Get TREND weather infos
        /// </summary>
        /// <param name="trendTokens">TREND tokens</param>
        /// <param name="month">Current month</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        public static Trend[] GetTrends(string[] trendTokens, Month month, List<string> errors)
        {
            if (trendTokens is null or { Length: 0 })
                return null;

            var trendReports = Recognizer.Instance().RecognizeAndGroupTokensTrend(trendTokens);
            var outcome = new List<Trend>();

            foreach (var report in trendReports)
            {
                var current = new Trend(report, month);
                if (current.ParseErrors is { Length: > 0 })
                {
                    errors = errors.Concat(current.ParseErrors).ToList();
                    continue;
                }

                outcome.Add(current);
            }

            return outcome.ToArray();
        }
    }
}
