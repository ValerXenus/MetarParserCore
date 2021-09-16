using MetarParcerCore.Enums;

namespace MetarParcerCore.Objects
{
    /// <summary>
    /// Date and time of the airport by Zulu
    /// </summary>
    public class ZuluDateTime
    {
        /// <summary>
        /// Current month
        /// </summary>
        public Month Month { get; init; }

        /// <summary>
        /// Day of the current month
        /// </summary>
        public int Day { get; init; }

        /// <summary>
        /// Hours
        /// </summary>
        public int Hours { get; init; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; init; }
    }
}
