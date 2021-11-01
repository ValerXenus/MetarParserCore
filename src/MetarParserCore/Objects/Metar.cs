using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General METAR data class
    /// NOTE: Any property can be null
    /// </summary>
    public class Metar
    {
        /// <summary>
        /// METAR report type
        /// </summary>
        public ReportType ReportType { get; init; }

        /// <summary>
        /// Report is empty
        /// </summary>
        public bool IsNil { get; init; }

        /// <summary>
        /// Airport ICAO code
        /// </summary>
        public string Airport { get; init; }

        /// <summary>
        /// Date and time by Zulu of the observation
        /// </summary>
        public ObservationDayTime ObservationDayTime { get; init; }

        /// <summary>
        /// Current month
        /// </summary>
        public Month Month { get; init; }

        /// <summary>
        /// METAR modifier
        /// </summary>
        public MetarModifier Modifier { get; init; }

        /// <summary>
        /// Info about surface wind
        /// </summary>
        public SurfaceWind SurfaceWind { get; init; }

        /// <summary>
        /// Info about visibility
        /// </summary>
        public PrevailingVisibility PrevailingVisibility { get; init; }

        /// <summary>
        /// Info about visibility on runways (RVR)
        /// </summary>
        public RunwayVisualRange[] RunwayVisualRanges { get; init; }

        /// <summary>
        /// Special weather conditions
        /// </summary>
        public WeatherPhenomena[] PresentWeather { get; init; }

        /// <summary>
        /// Info about clouds (Cloud layers)
        /// </summary>
        public CloudLayer[] CloudLayers { get; init; }

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
        public Trend Trend { get; init; }

        /// <summary>
        /// Fog dispersal operations are in progress
        /// </summary>
        public bool IsDeneb { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        public string Remarks { get; init; }

        /// <summary>
        /// Set of parse errors
        /// </summary>
        public string[] ParseErrors { get; init; }

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
        {
            if (groupedTokens.Count == 0)
            {
                ParseErrors = new[] { "Grouped tokens dictionary is empty" };
                return;
            }

            var errors = new List<string>();

            ReportType = getMetarReportType(groupedTokens);
            Airport = getAirportIcao(groupedTokens, errors);
            ObservationDayTime =
                getDataObjectOrNull<ObservationDayTime>(
                    groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime), errors);
            Month = currentMonth;
            Modifier = getMetarModifier(groupedTokens);
            SurfaceWind =
                getDataObjectOrNull<SurfaceWind>(groupedTokens.GetTokenGroupOrDefault(TokenType.SurfaceWind),
                    errors);
            PrevailingVisibility =
                getDataObjectOrNull<PrevailingVisibility>(groupedTokens.GetTokenGroupOrDefault(TokenType.PrevailingVisibility),
                    errors);
            RunwayVisualRanges =
                getParsedDataArray<RunwayVisualRange>(groupedTokens.GetTokenGroupOrDefault(TokenType.RunwayVisualRange),
                    errors);
            PresentWeather =
                getParsedDataArray<WeatherPhenomena>(groupedTokens.GetTokenGroupOrDefault(TokenType.PresentWeather),
                    errors);
            CloudLayers =
                getParsedDataArray<CloudLayer>(groupedTokens.GetTokenGroupOrDefault(TokenType.CloudLayer), 
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

            // Parser errors
            ParseErrors = errors.Count == 0 ? null : errors.ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get report type
        /// METAR - default
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <returns></returns>
        private ReportType getMetarReportType(Dictionary<TokenType, string[]> groupedTokens)
        {
            var reportType = groupedTokens.GetTokenGroupOrDefault(TokenType.ReportType);
            if (reportType is null or {Length: 0})
                return ReportType.Metar;

            switch (reportType[0])
            {
                case "SPECI":
                    return ReportType.Speci;
                case "TAF":
                    return ReportType.Taf;
                default:
                    return ReportType.Metar;
            }
        }

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
        /// Get METAR report modifier
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <returns></returns>
        private MetarModifier getMetarModifier(Dictionary<TokenType, string[]> groupedTokens)
        {
            var modifierValue = groupedTokens.GetTokenGroupOrDefault(TokenType.Modifier);
            if (modifierValue.Length == 0)
                return MetarModifier.None;

            return modifierValue[0].Equals("AUTO")
                ? MetarModifier.Auto
                : MetarModifier.Cor;
        }

        /// <summary>
        /// Provides parsed data object or null if parse error occur
        /// </summary>
        /// <typeparam name="T">For object type</typeparam>
        /// <param name="groupTokens">Current group of tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        private T getDataObjectOrNull<T>(string[] groupTokens, List<string> errors)
            where T : class
        {
            if (groupTokens.Length == 0)
                return null;

            var previousErrorsCount = errors.Count;
            var data = (T) Activator.CreateInstance(typeof(T), BindingFlags.NonPublic | BindingFlags.Instance, null,
                new object[] {groupTokens, errors}, null);

            return errors.Count - previousErrorsCount > 1 
                ? null 
                : data;
        }

        /// <summary>
        /// Get parsed data array from tokens array
        /// </summary>
        /// <typeparam name="T">Class name</typeparam>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">Errors list</param>
        /// <returns></returns>
        private T[] getParsedDataArray<T>(string[] tokens, List<string> errors) 
            where T : class
        {
            var outcome = tokens.Select(x => getDataObjectOrNull<T>(new[] { x }, errors))
                .ToArray();
            return outcome.Length != 0
                ? outcome
                : null;
        }

        #endregion
    }
}
