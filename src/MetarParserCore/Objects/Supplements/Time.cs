namespace MetarParserCore.Objects.Supplements
{
    /// <summary>
    /// Custom time class
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Hours
        /// </summary>
        public int Hours { get; init; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public Time() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        internal Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        #endregion
    }
}
