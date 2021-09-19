using System.Collections.Generic;
using MetarParserCore.Enums;

namespace MetarParserCore.TokenLogic
{
    /// <summary>
    /// Transforms array of tokens into groups which represented in dictionary
    /// </summary>
    internal class TokenGrouper
    {
        #region Public methods

        /// <summary>
        /// Transform method
        /// </summary>
        /// <param name="tokens">Array of recognized tokens</param>
        /// <returns></returns>
        public Dictionary<TokenType, string[]> TransformToGroups(Token[] tokens)
        {
            var outcomeDictionary = new Dictionary<TokenType, string[]>();
            var currentTokensGroup = new List<string>();
            var lastTokenType = TokenType.ReportType;
            var groupMode = false;

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                if (token.Type == TokenType.Motne || i == tokens.Length - 1)
                {
                    saveGroupInDictionary(token, currentTokensGroup, outcomeDictionary, ref lastTokenType);
                    groupMode = false;
                    continue;
                }

                if (isGroupableType(token.Type))
                {
                    saveGroupInDictionary(token, currentTokensGroup, outcomeDictionary, ref lastTokenType);
                    groupMode = true;
                    continue;
                }

                if (token.Type == lastTokenType || groupMode)
                {
                    currentTokensGroup.Add(token.Value);
                    continue;
                }

                if (currentTokensGroup.Count == 0)
                {
                    lastTokenType = token.Type;
                    currentTokensGroup.Add(token.Value);
                    continue;
                }

                saveGroupInDictionary(token, currentTokensGroup, outcomeDictionary, ref lastTokenType);
            }

            return outcomeDictionary;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Determines that current type is groupable (has a chain of several tokens after declare)
        /// </summary>
        /// <param name="type">Current token type</param>
        /// <returns></returns>
        private bool isGroupableType(TokenType type) =>
            type is TokenType.RecentWeather 
                or TokenType.WindShear 
                or TokenType.Trend 
                or TokenType.Remarks;

        /// <summary>
        /// Save current group
        /// </summary>
        /// <param name="token">Current token</param>
        /// <param name="lastTokenType">Last token type</param>
        /// <param name="currentTokensGroup">List of current tokens group</param>
        /// <param name="outcomeDictionary">Result dictionary</param>
        private void saveGroupInDictionary(Token token, List<string> currentTokensGroup,
            IDictionary<TokenType, string[]> outcomeDictionary, ref TokenType lastTokenType)
        {
            outcomeDictionary.Add(lastTokenType, currentTokensGroup.ToArray());

            currentTokensGroup.Clear();
            currentTokensGroup.Add(token.Value);
            lastTokenType = token.Type;
        }

        #endregion
    }
}
