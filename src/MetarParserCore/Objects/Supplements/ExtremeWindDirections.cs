namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// Info about two extreme wind directions
    /// during the 10 minute period of the observation
    /// </summary>
    public class ExtremeWindDirections
    {
        /// <summary>
        /// First value of the extreme wind direction interval
        /// </summary>
        public int FirstExtremeDirection { get; init; }

        /// <summary>
        /// Last value of the extreme wind direction interval
        /// </summary>
        public int LastExtremeWindDirection { get; init; }
    }
}
