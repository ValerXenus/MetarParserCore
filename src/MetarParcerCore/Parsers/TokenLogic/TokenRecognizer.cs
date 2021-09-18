using System.Linq;
using System.Text.RegularExpressions;
using MetarParserCore.Enums;

namespace MetarParserCore.Parsers.TokenLogic
{
    /// <summary>
    /// Class for recognition METAR tokens
    /// </summary>
    internal class TokenRecognizer
    {
        #region Fields

        /// <summary>
        /// Sign if airport has been recognized
        /// </summary>
        private bool _isAirportRecognized;

        #endregion

        public TokenRecognizer()
        {
            _isAirportRecognized = false;
        }

        #region Public methods

        /// <summary>
        /// Recognize raw tokens
        /// </summary>
        /// <param name="rawTokens"></param>
        /// <returns></returns>
        public Token[] RecognizeTokens(string[] rawTokens)
        {
            return rawTokens.Select(recognizeAndCreateToken).ToArray();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Recognize current token
        /// </summary>
        /// <param name="rawToken">Current raw value</param>
        /// <returns></returns>
        private Token recognizeAndCreateToken(string rawToken)
        {
            switch (rawToken)
            {
                case "METAR":
                case "SPECI":
                    return new Token(TokenType.Special, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.Airport) && !_isAirportRecognized:
                    _isAirportRecognized = true;
                    return new Token(TokenType.Airport, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.ObservationDayTime):
                    return new Token(TokenType.ObservationDayTime, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.Modifier):
                    return new Token(TokenType.Modifier, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.SurfaceWind):
                    return new Token(TokenType.SurfaceWind, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.PrevailingVisibility):
                    return new Token(TokenType.PrevailingVisibility, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.RunwayVisualRange):
                    return new Token(TokenType.RunwayVisualRange, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.PresentWeather):
                    return new Token(TokenType.PresentWeather, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.CloudLayers):
                    return new Token(TokenType.CloudLayers, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.Temperature):
                    return new Token(TokenType.Temperature, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.AltimeterSetting):
                    return new Token(TokenType.AltimeterSetting, rawToken);
                case { } when Regex.IsMatch(rawToken, TokenRegex.Motne):
                    return new Token(TokenType.Motne, rawToken);
                case { } when rawToken.StartsWith("RE"):
                    return new Token(TokenType.RecentWeather, rawToken);
                case "WS":
                case "DENEB":
                    return new Token(TokenType.WindShear, rawToken);
                case "BECMG":
                case "TEMPO":
                case "NOSIG":
                    return new Token(TokenType.Trend, rawToken);
                case "RMK":
                    return new Token(TokenType.Remarks, rawToken);
            }

            return new Token(TokenType.Unknown, rawToken);
        }

        #endregion
    }
}
