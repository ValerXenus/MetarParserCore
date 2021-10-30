using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Info about clouds and vertical visibility (Cloud layers)
    /// </summary>
    public class CloudLayer
    {
        /// <summary>
        /// Cloud type
        /// </summary>
        public CloudType CloudType { get; init; }

        /// <summary>
        /// Cloud altitude
        /// </summary>
        public int Altitude { get; init; }

        /// <summary>
        /// Convective cloud type
        /// </summary>
        public ConvectiveCloudType ConvectiveCloudType { get; init; }

        /// <summary>
        /// Cloud below airport (in mountain airports)
        /// </summary>
        public bool IsCloudBelow { get; init; }

        /// <summary>
        /// Default
        /// </summary>
        public CloudLayer() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal CloudLayer(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of cloud layer tokens is empty");
                return;
            }

            var cloudToken = tokens.First();

            CloudType = parseCloudType(ref cloudToken);
            if (CloudType is CloudType.SkyClear 
                or CloudType.Clear 
                or CloudType.NoCloudDetected 
                or CloudType.NoSignificantClouds)
                return;

            Altitude = getAltitude(ref cloudToken, errors, out var isCloudBelow);
            IsCloudBelow = isCloudBelow;
            ConvectiveCloudType = getConvectiveCloudType(cloudToken);
        }

        /// <summary>
        /// Parse the current cloud type
        /// </summary>
        /// <param name="token">String token</param>
        /// <returns></returns>
        private CloudType parseCloudType(ref string token)
        {
            if (token.StartsWith("VV"))
            {
                token = token.Replace("VV", "");
                return CloudType.VerticalVisibility;
            }

            var outcome = EnumTranslator.GetValueFromDescription<CloudType>(token[..3]);
            token = token[3..];
            return outcome;
        }

        /// <summary>
        /// Get FL altitude of the cloud
        /// </summary>
        /// <param name="token">String token</param>
        /// <param name="errors">Errors list</param>
        /// <param name="isCloudBelow">Sign if cloud is below airport</param>
        /// <returns></returns>
        private int getAltitude(ref string token, List<string> errors, out bool isCloudBelow)
        {
            isCloudBelow = false;

            if (token.StartsWith("///"))
            {
                isCloudBelow = true;
                return 0;
            }

            if (!int.TryParse(token[..3], out var altitude))
            {
                errors.Add($"Cannot parse altitude from token {token}");
                return 0;
            }

            token = token[3..];
            return altitude;
        }

        /// <summary>
        /// Get convective cloud type
        /// </summary>
        /// <param name="token">String token</param>
        /// <returns></returns>
        private ConvectiveCloudType getConvectiveCloudType(string token)
        {
            return string.IsNullOrEmpty(token)
                ? ConvectiveCloudType.None
                : EnumTranslator.GetValueFromDescription<ConvectiveCloudType>(token);
        }
    }
}