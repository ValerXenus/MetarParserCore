using System;
using MetarParserCore.Enums;

namespace MetarParserCore
{
    /// <summary>
    /// Main class intended for start parse process of raw METAR
    /// </summary>
    public class MetarParser
    {
        /// <summary>
        /// Parse method
        /// </summary>
        /// <param name="raw">Raw METAR string</param>
        /// <param name="month">Current month</param>
        /// <returns>True - if parse successful, False - otherwise</returns>
        public static bool Parse(string raw, Month month = Month.None)
        {
            throw new NotImplementedException();
        }
    }
}
