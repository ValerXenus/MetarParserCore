﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using MetarParserCore.Common;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General METAR data class
    /// NOTE: Any property can be null
    /// </summary>
    [DataContract(Name = "metar")]
    public class Metar : ReportBase
    {
        /// <summary>
        /// Airport ICAO code
        /// </summary>
        [DataMember(Name = "airport", EmitDefaultValue = false)]
        public string Airport { get; init; }

        /// <summary>
        /// Date and time by Zulu of the observation
        /// </summary>
        [DataMember(Name = "observationDayTime", EmitDefaultValue = false)]
        public ObservationDayTime ObservationDayTime { get; init; }

        /// <summary>
        /// Info about visibility on runways (RVR)
        /// </summary>
        [DataMember(Name = "runwayVisualRanges", EmitDefaultValue = false)]
        public RunwayVisualRange[] RunwayVisualRanges { get; init; }

        /// <summary>
        /// Information about temperature
        /// </summary>
        [DataMember(Name = "temperature", EmitDefaultValue = false)]
        public TemperatureInfo Temperature { get; init; }

        /// <summary>
        /// Information about air pressure
        /// </summary>
        [DataMember(Name = "altimeterSetting", EmitDefaultValue = false)]
        public AltimeterSetting AltimeterSetting { get; init; }

        /// <summary>
        /// Recent weather info
        /// </summary>
        [DataMember(Name = "recentWeather", EmitDefaultValue = false)]
        public WeatherPhenomena RecentWeather { get; init; }

        /// <summary>
        /// Wind shear info
        /// </summary>
        [DataMember(Name = "windShear", EmitDefaultValue = false)]
        public WindShear WindShear { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        [DataMember(Name = "motnes", EmitDefaultValue = false)]
        public Motne[] Motne { get; init; }

        /// <summary>
        /// Info about sea-surface temperature and state
        /// </summary>
        [DataMember(Name = "seaCondition", EmitDefaultValue = false)]
        public SeaCondition SeaCondition { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        [DataMember(Name = "trends", EmitDefaultValue = false)]
        public Trend[] Trends { get; init; }

        /// <summary>
        /// Fog dispersal operations are in progress
        /// </summary>
        [DataMember(Name = "isDeneb", EmitDefaultValue = false)]
        public bool IsDeneb { get; init; }

        /// <summary>
        /// Military airfield weather (represents in color codes)
        /// </summary>
        [DataMember(Name = "militaryWeather", EmitDefaultValue = false)]
        public MilitaryWeather MilitaryWeather { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        [DataMember(Name = "remarks", EmitDefaultValue = false)]
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
        internal Metar(Dictionary<TokenType, string[]> groupedTokens, Month currentMonth) : base(groupedTokens, currentMonth)
        {
            var errors = new List<string>();

            ReportType = ReportType.Metar;
            Airport = GroupExtractor.GetAirportIcao(groupedTokens, errors);
            ObservationDayTime = GetDataObjectOrNull<ObservationDayTime>(groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime), errors);
            RunwayVisualRanges = GetParsedDataArray<RunwayVisualRange>(groupedTokens.GetTokenGroupOrDefault(TokenType.RunwayVisualRange), errors);
            Temperature = GetDataObjectOrNull<TemperatureInfo>(groupedTokens.GetTokenGroupOrDefault(TokenType.Temperature), errors);
            AltimeterSetting = GetDataObjectOrNull<AltimeterSetting>(groupedTokens.GetTokenGroupOrDefault(TokenType.AltimeterSetting), errors);
            RecentWeather = GetDataObjectOrNull<WeatherPhenomena>(groupedTokens.GetTokenGroupOrDefault(TokenType.RecentWeather), errors);
            WindShear = GetDataObjectOrNull<WindShear>(groupedTokens.GetTokenGroupOrDefault(TokenType.WindShear), errors);
            Motne = GetParsedDataArray<Motne>(groupedTokens.GetTokenGroupOrDefault(TokenType.Motne), errors);
            SeaCondition = GetDataObjectOrNull<SeaCondition>(groupedTokens.GetTokenGroupOrDefault(TokenType.SeaState), errors);
            IsDeneb = groupedTokens.ContainsKey(TokenType.Deneb);
            Trends = GroupExtractor.GetTrends(groupedTokens.GetTokenGroupOrDefault(TokenType.Trend), Month, errors);
            MilitaryWeather = GetDataObjectOrNull<MilitaryWeather>(groupedTokens.GetTokenGroupOrDefault(TokenType.MilitaryColorCode), errors);
            Remarks = GetRemarks(groupedTokens.GetTokenGroupOrDefault(TokenType.Remarks));

            ParseErrors = errors.Count == 0 ? null : errors.ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get remarks as string
        /// </summary>
        /// <param name="remarkTokens">Array of tokens</param>
        /// <returns></returns>
        private string GetRemarks(string[] remarkTokens)
        {
            if (remarkTokens is null or { Length: 0 })
                return null;

            remarkTokens = remarkTokens[1..];
            return string.Join(" ", remarkTokens);
        }

        #endregion
    }
}
