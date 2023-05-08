using System.Runtime.Serialization;

namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// TAF temperature data value
    /// </summary>
    [DataContract(Name = "tafTemperatureValue")]
    public class TafTemperatureValue
    {
        /// <summary>
        /// Temperature value in Celsius
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public int Value { get; init; }

        /// <summary>
        /// A day for which the forecast is represented
        /// </summary>
        [DataMember(Name = "day", EmitDefaultValue = false)]
        public int Day { get; init; }

        /// <summary>
        /// A time in hours for which the forecast is represented (in Zulu)
        /// </summary>
        [DataMember(Name = "hours", EmitDefaultValue = false)]
        public int Hours { get; init; }
    }
}
