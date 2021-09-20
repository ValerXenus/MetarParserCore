using System;
using System.Collections.Generic;
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
        /// Info about runway visibility (RVR)
        /// </summary>
        public RunwayVisualRange RunwayVisualRange { get; init; }

        /// <summary>
        /// Special weather conditions
        /// </summary>
        public PresentWeather PresentWeather { get; init; }

        /// <summary>
        /// Info about clouds (Cloud layers)
        /// </summary>
        public CloudLayers CloudLayers { get; init; }

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
        public RecentWeather RecentWeather { get; init; }

        /// <summary>
        /// Wind shear info
        /// </summary>
        public string[] WindShear { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        public Motne Motne { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        public Trend Trend { get; init; }

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
        public Metar(Dictionary<TokenType, string[]> groupedTokens, Month currentMonth)
        {
            if (groupedTokens.Count == 0)
            {
                ParseErrors = new[] { "Grouped tokens dictionary is empty" };
                return;
            }

            var errors = new List<string>();

            Airport = getAirportIcao(groupedTokens, errors);
            ObservationDayTime = getObservationDayTime(groupedTokens, errors, currentMonth);
            Modifier = getMetarModifier(groupedTokens);
            SurfaceWind = getDataObjectOrNull<SurfaceWind>(groupedTokens, errors);
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
        /// Get metar report modifier
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
        /// Get observation day time data object
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <param name="currentMonth">Current month</param>
        /// <returns></returns>
        private ObservationDayTime getObservationDayTime(Dictionary<TokenType, string[]> groupedTokens,
            List<string> errors, Month currentMonth)
        {
            var previousErrorsCount = errors.Count;
            var data = new ObservationDayTime(groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime),
                errors, currentMonth);

            if (errors.Count - previousErrorsCount > 1)
                return null;

            return data;
        }

        /// <summary>
        /// Provides parsed data object or null if parse error occur
        /// </summary>
        /// <typeparam name="T">For object type</typeparam>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="errors">List of parse errors</param>
        /// <returns></returns>
        private T getDataObjectOrNull<T>(Dictionary<TokenType, string[]> groupedTokens,
            List<string> errors) where T : class
        {
            var previousErrorsCount = errors.Count;
            var data = (T)Activator.CreateInstance(typeof(T), groupedTokens.GetTokenGroupOrDefault(TokenType.SurfaceWind),
                errors);

            if (errors.Count - previousErrorsCount > 1)
                return null;

            return data;
        }

        #endregion
    }
}
