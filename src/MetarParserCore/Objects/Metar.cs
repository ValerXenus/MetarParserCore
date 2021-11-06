using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;
using MetarParserCore.TokenLogic;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General METAR data class
    /// NOTE: Any property can be null
    /// </summary>
    public class Metar : ReportBase
    {
        /// <summary>
        /// Airport ICAO code
        /// </summary>
        public string Airport { get; init; }

        /// <summary>
        /// Date and time by Zulu of the observation
        /// </summary>
        public ObservationDayTime ObservationDayTime { get; init; }

        /// <summary>
        /// Info about visibility on runways (RVR)
        /// </summary>
        public RunwayVisualRange[] RunwayVisualRanges { get; init; }

        /// <summary>
        /// Information about temperature
        /// </summary>
        public TemperatureInfo Temperature { get; init; }

        /// <summary>
        /// Information about air pressure
        /// </summary>
        public AltimeterSetting AltimeterSetting { get; init; }

        /// <summary>
        /// Recent weather info
        /// </summary>
        public WeatherPhenomena RecentWeather { get; init; }

        /// <summary>
        /// Wind shear info
        /// </summary>
        public WindShear WindShear { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        public Motne[] Motne { get; init; }

        /// <summary>
        /// Info about sea-surface temperature and state
        /// </summary>
        public SeaCondition SeaCondition { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        public Trend[] Trends { get; init; }

        /// <summary>
        /// Fog dispersal operations are in progress
        /// </summary>
        public bool IsDeneb { get; init; }

        /// <summary>
        /// Military airfield weather (represents in color codes)
        /// </summary>
        public MilitaryWeather MilitaryWeather { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        public string Remarks { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public Metar() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="currentMonth">Current month</param>
        internal Metar(Dictionary<TokenType, string[]> groupedTokens, Month currentMonth) 
            : base(groupedTokens, currentMonth)
        {
            var errors = new List<string>();

            Airport = getAirportIcao(groupedTokens, errors);
            ObservationDayTime =
                getDataObjectOrNull<ObservationDayTime>(
                    groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime), errors);
            RunwayVisualRanges =
                getParsedDataArray<RunwayVisualRange>(groupedTokens.GetTokenGroupOrDefault(TokenType.RunwayVisualRange),
                    errors);
            Temperature =
                getDataObjectOrNull<TemperatureInfo>(groupedTokens.GetTokenGroupOrDefault(TokenType.Temperature),
                    errors);
            AltimeterSetting =
                getDataObjectOrNull<AltimeterSetting>(groupedTokens.GetTokenGroupOrDefault(TokenType.AltimeterSetting),
                    errors);
            RecentWeather =
                getDataObjectOrNull<WeatherPhenomena>(groupedTokens.GetTokenGroupOrDefault(TokenType.RecentWeather),
                    errors);
            WindShear =
                getDataObjectOrNull<WindShear>(groupedTokens.GetTokenGroupOrDefault(TokenType.WindShear),
                    errors);
            Motne =
                getParsedDataArray<Motne>(groupedTokens.GetTokenGroupOrDefault(TokenType.Motne),
                    errors);
            SeaCondition =
                getDataObjectOrNull<SeaCondition>(groupedTokens.GetTokenGroupOrDefault(TokenType.SeaState),
                    errors);
            IsDeneb = groupedTokens.ContainsKey(TokenType.Deneb);
            Trends = getTrends(groupedTokens.GetTokenGroupOrDefault(TokenType.Trend), errors);
            MilitaryWeather =
                getDataObjectOrNull<MilitaryWeather>(groupedTokens.GetTokenGroupOrDefault(TokenType.MilitaryColorCode),
                    errors);

            // Parser errors
            ParseErrors = errors.Count == 0 ? null : errors.ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get airport ICAO code
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        private string getAirportIcao(Dictionary<TokenType, string[]> groupedTokens, List<string> errors)
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
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        private Trend[] getTrends(string[] trendTokens, List<string> errors)
        {
            var trendReports = Recognizer.Instance().RecognizeAndGroupTokensTrend(trendTokens);
            var outcome = new List<Trend>();

            foreach (var report in trendReports)
            {
                var current = new Trend(report, Month);
                if (current.ParseErrors is { Length: > 0 })
                {
                    errors = errors.Concat(current.ParseErrors).ToList();
                    continue;
                }

                outcome.Add(current);
            }

            return outcome.ToArray();
        }

        #endregion
    }
}
