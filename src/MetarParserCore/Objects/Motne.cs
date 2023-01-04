using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Info about runway conditions
    /// </summary>
    public class Motne
    {
        /// <summary>
        /// Current runway
        /// </summary>
        public string RunwayNumber { get; init; }

        /// <summary>
        /// MOTNE special sign
        /// </summary>
        public MotneSpecials Specials { get; init; }

        /// <summary>
        /// Type of deposit
        /// </summary>
        public MotneTypeOfDeposit TypeOfDeposit { get; init; }

        /// <summary>
        /// Extent of contamination of the current runway
        /// </summary>
        public MotneExtentOfContamination ExtentOfContamination { get; init; }

        /// <summary>
        /// Depth of deposit (2 digits)
        /// -1 - depth not significant (has value "//")
        /// </summary>
        public int DepthOfDeposit { get; init; }

        /// <summary>
        /// Braking conditions
        /// -1 - not measured (has value "//")
        /// </summary>
        public int FrictionCoefficient { get; init; }

        /// <summary>
        /// Default
        /// </summary>
        public Motne() { }

        /// <summary>
        /// Parse constructor
        /// </summary>
        internal Motne(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Motne token is not found in incoming array");
                return;
            }

            var motneToken = tokens.First();
            if (string.IsNullOrEmpty(motneToken))
            {
                errors.Add("Motne token was not found");
                return;
            }

            switch (motneToken)
            {
                case { } when motneToken.Contains("CLRD"):
                    FrictionCoefficient = GetMotneIntValue(motneToken, motneToken.Length - 2, 2);
                    RunwayNumber = GetRunwayNumber(ref motneToken, errors);
                    Specials = MotneSpecials.Cleared;
                    return;
                case { } when motneToken.Contains("CLSD"):
                    FrictionCoefficient = GetMotneIntValue(motneToken, motneToken.Length - 2, 2);
                    RunwayNumber = GetRunwayNumber(ref motneToken, errors);
                    Specials = MotneSpecials.Closed;
                    return;
                case { } when motneToken.Contains("SNOCLO"):
                    Specials = MotneSpecials.ClosedToSnow;
                    return;
            }

            RunwayNumber = GetRunwayNumber(ref motneToken, errors);
            TypeOfDeposit = motneToken.Substring(0, 1).Equals("/")
                ? MotneTypeOfDeposit.NotReported
                : GetMotneEnum<MotneTypeOfDeposit>(motneToken.Substring(0, 1));
            ExtentOfContamination = GetMotneEnum<MotneExtentOfContamination>(motneToken.Substring(1, 1));
            DepthOfDeposit = GetMotneIntValue(motneToken, 2, 2);
            FrictionCoefficient = GetMotneIntValue(motneToken, motneToken.Length - 2, 2);
            Specials = MotneSpecials.Default;
        }

        /// <summary>
        /// Parse runway number
        /// <param name="motneToken">Current MOTNE</param>
        /// <param name="errors">Errors list</param>
        /// </summary>
        /// <returns></returns>
        private string GetRunwayNumber(ref string motneToken, List<string> errors)
        {
            if (motneToken.StartsWith("R"))
            {
                var splittedMotne = motneToken.Split("/");
                motneToken = motneToken[(splittedMotne[0].Length + 1)..];
                return splittedMotne[0][1..];
            }

            var stringNumber = motneToken[..2];
            var runwayNumber = int.Parse(stringNumber);
            if (runwayNumber > 86 && runwayNumber != 88 && runwayNumber != 99)
            {
                errors.Add($"Incorrect runway number in MOTNE {motneToken} token");
                return string.Empty;
            }

            motneToken = motneToken[2..];
            return stringNumber;
        }

        /// <summary>
        /// Get MOTNE data as enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringValue">MOTNE string value</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private T GetMotneEnum<T>(string stringValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (stringValue.Contains("/"))
                return default;

            return (T)(object)int.Parse(stringValue);
        }

        /// <summary>
        /// Get MOTNE data as integer value
        /// </summary>
        /// <param name="motneToken">Current MOTNE token</param>
        /// <param name="startIdx">Substring start index</param>
        /// <param name="length">Elements length</param>
        /// <returns></returns>
        private int GetMotneIntValue(string motneToken, int startIdx, int length)
        {
            var valueToken = motneToken.Substring(startIdx, length);
            if (valueToken.Contains("/"))
                return -1;

            return int.Parse(valueToken);
        }
    }
}
