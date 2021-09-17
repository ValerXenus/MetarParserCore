using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetarParserCore.Enums;

namespace MetarParserCore.Parsers
{
    /// <summary>
    /// Class for recognize METAR tokens
    /// </summary>
    public class Recognizer
    {
        #region Fields

        private TokenType _tokenState;

        #endregion

        public Recognizer()
        {
            _tokenState = TokenType.Airport;
        }

        #region Public methods

        public Token[] RecognizeTokens(string[] rawTokens)
        {
            var tokens = new Token[rawTokens.Length];
            var tokenIdx = 0;

            while (true)
            {
                var rawToken = rawTokens[tokenIdx];
                switch (_tokenState)
                {
                    case TokenType.Airport:
                        if (rawToken.Equals("METAR") || rawToken.Equals("SPECI"))
                        {
                            tokens[tokenIdx++] = new Token(TokenType.Special, rawToken);
                            break;
                        }
                        tokens[tokenIdx++] = validateAndCreateToken(rawToken, TokenRegex.Airport);
                        break;

                    case TokenType.ObservationDayTime:
                        tokens[tokenIdx++] = validateAndCreateToken(rawToken, TokenRegex.ObservationDayTime);
                        break;

                    case TokenType.Modifier:
                        if (!Regex.IsMatch(rawToken, TokenRegex.Modifier))
                        {
                            _tokenState++;
                            continue;
                        }
                        tokens[tokenIdx++] = new Token(_tokenState, rawToken);
                        break;

                    case TokenType.SurfaceWind:
                        tokens[tokenIdx++] = validateAndCreateToken(rawToken, TokenRegex.SurfaceWind);
                        break;
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validate current token using suitable regex pattern and create recognized token
        /// </summary>
        /// <param name="rawToken">Current raw value</param>
        /// <param name="regexPattern">Current regex pattern</param>
        /// <returns></returns>
        private Token validateAndCreateToken(string rawToken, string regexPattern)
        {
            return !Regex.IsMatch(rawToken, regexPattern)
                ? new Token(TokenType.Unexpected, rawToken)
                : new Token(_tokenState++, rawToken);
        }

        #endregion
    }
}
