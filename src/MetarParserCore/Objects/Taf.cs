using MetarParserCore.Common;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General TAF data class
    /// NOTE: Any property can be null
    /// </summary>
    [DataContract(Name = "taf")]
    public class Taf: ReportBase
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
        /// Represented forecast period for TAF reports
        /// </summary>
        [DataMember(Name = "tafForecastPeriod", EmitDefaultValue = false)]
        public TafForecastPeriod TafForecastPeriod { get; init; }

        /// <summary>
        /// Sign "CNL" which means report with that specified forecast period was cancelled
        /// </summary>
        [DataMember(Name = "isReportCancelled", EmitDefaultValue = false)]
        public bool IsReportCancelled { get; set; }

        /// <summary>
        /// TAF forecast temperature info
        /// </summary>
        [DataMember(Name = "tafTemperatureInfo", EmitDefaultValue = false)]
        public TafTemperatureInfo TafTemperatureInfo { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public Taf() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="groupedTokens">Dictionary of grouped tokens</param>
        /// <param name="currentMonth">Current month</param>
        internal Taf(Dictionary<TokenType, string[]> groupedTokens, Month currentMonth) : base(groupedTokens, currentMonth)
        {
            var errors = new List<string>();

            ReportType = ReportType.Taf;
            Airport = GroupExtractor.GetAirportIcao(groupedTokens, errors);
            ObservationDayTime = GetDataObjectOrNull<ObservationDayTime>(groupedTokens.GetTokenGroupOrDefault(TokenType.ObservationDayTime), errors);
            TafForecastPeriod = GetDataObjectOrNull<TafForecastPeriod>(groupedTokens.GetTokenGroupOrDefault(TokenType.TafForecastPeriod), errors);
            IsReportCancelled = groupedTokens.ContainsKey(TokenType.Cnl);
            TafTemperatureInfo = GetDataObjectOrNull<TafTemperatureInfo>(groupedTokens.GetTokenGroupOrDefault(TokenType.TafTemperature), errors);

            ParseErrors = errors.Count == 0 ? null : errors.ToArray();
        }

        #endregion
    }
}
