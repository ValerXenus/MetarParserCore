using System.Runtime.Serialization;

namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// TAF day time value which using in periods
    /// </summary>
    [DataContract(Name = "tafDayTime")]
    public class TafDayTime
    {
        /// <summary>
        /// Day of the current month
        /// </summary>
        [DataMember(Name = "day", EmitDefaultValue = false)]
        public int Day { get; init; }

        /// <summary>
        /// Time in hours
        /// </summary>
        [DataMember(Name = "hours", EmitDefaultValue = false)]
        public int Hours { get; init; }
    }
}
