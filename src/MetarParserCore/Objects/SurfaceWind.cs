﻿using System.Collections.Generic;
using MetarParserCore.Enums;
using MetarParserCore.Objects.Supplements;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Surface wind information
    /// </summary>
    public class SurfaceWind
    {
        /// <summary>
        /// Current wind direction
        /// </summary>
        public int Direction { get; init; }

        /// <summary>
        /// Sign if wind has variable direction (VRB)
        /// </summary>
        public bool IsVariable { get; init; }

        /// <summary>
        /// Speed of the wind
        /// </summary>
        public int Speed { get; init; }

        /// <summary>
        /// Max wind speed or gust speed
        /// </summary>
        public int GustSpeed { get; init; }

        /// <summary>
        /// Type of wind unit
        /// </summary>
        public WindUnit WindUnit { get; init; }

        /// <summary>
        /// Info about extreme wind directions
        /// </summary>
        public ExtremeWindDirections ExtremeWindDirections { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public SurfaceWind() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal SurfaceWind(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Wind tokens not found");
                return;
            }

            var windValue = tokens[0].Substring(0, 3);
            if (windValue.Equals("VRB"))
                IsVariable = true;
            else
            {
                IsVariable = false;
                Direction = int.Parse(windValue);
            }

            Speed = int.Parse(tokens[0].Substring(3, 2));

            if (tokens[0].Substring(5, 1).Equals("G"))
                GustSpeed = int.Parse(tokens[0].Substring(6, 2));

            WindUnit = getWindUnit(tokens[0]);

            if (tokens.Length > 1) 
                ExtremeWindDirections = getExtremeWindDirections(tokens[1]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get wind unit from the token
        /// </summary>
        /// <param name="unitString"></param>
        /// <returns></returns>
        private WindUnit getWindUnit(string unitString)
        {
            if (unitString.EndsWith("KMT"))
                return WindUnit.KilometersPerHour;
            if (unitString.EndsWith("KT"))
                return WindUnit.Knots;

            return WindUnit.MetersPerSecond;
        }

        /// <summary>
        /// Parse extreme wind directions token
        /// </summary>
        /// <param name="intervalToken"></param>
        /// <returns></returns>
        private ExtremeWindDirections getExtremeWindDirections(string intervalToken)
        {
            var directions = intervalToken.Split("V");
            return new ExtremeWindDirections
            {
                FirstExtremeDirection = int.Parse(directions[0]),
                LastExtremeWindDirection = int.Parse(directions[1])
            };
        }

        #endregion
    }
}
