using System.Runtime.Serialization;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// General TAF data class
    /// NOTE: Any property can be null
    /// </summary>
    [DataContract(Name = "metar")]
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
    }
}
