using System;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.Parsers.TokenLogic;

namespace MetarParserCore
{
    /// <summary>
    /// General METAR parser class
    /// </summary>
    public class MetarParser
    {
        #region Public methods

        /// <summary>
        /// Parse method
        /// </summary>
        /// <param name="raw">Raw METAR string</param>
        /// <param name="month">Current month</param>
        /// <returns>Parsed Metar object</returns>
        public Metar Parse(string raw, Month month = Month.None)
        {
            if (string.IsNullOrEmpty(raw))
                return new Metar
                {
                    ParseErrors = new []{ "Raw METAR is not correct" }
                };

            var unrecognizedTokens = raw.ToUpper().Split(" ");
            var recognizer = new TokenRecognizer();
            var tokens = recognizer.RecognizeTokens(unrecognizedTokens);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Multiple parse METARS method
        /// </summary>
        /// <param name="raws">Array of raw METAR strings</param>
        /// <returns>Array of parsed Metar objects</returns>
        public Metar[] Parse(string[] raws)
        {
            var metars = new Metar[raws.Length];
            for (var i = 0; i < raws.Length; i++)
            {
                metars[i] = Parse(raws[i]);
            }

            return metars;
        }

        #endregion

        #region Private methods


        #endregion
    }
}
