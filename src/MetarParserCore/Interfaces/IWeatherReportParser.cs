namespace MetarParserCore.Interfaces
{
    /// <summary>
    /// General METAR parser class
    /// </summary>
    public interface IWeatherReportParser<out T> where T : class
    {
        /// <summary>
        /// Parse method
        /// </summary>
        /// <param name="raw">Raw report string</param>
        /// <returns>Parsed report object</returns>
        T Parse(string raw);

        /// <summary>
        /// Multiple parse reports method
        /// </summary>
        /// <param name="raws">Array of raw report strings</param>
        /// <returns>Array of parsed report objects</returns>
        T[] Parse(string[] raws);
    }
}
