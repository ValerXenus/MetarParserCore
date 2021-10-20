﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Info about visibility on the runway (RVR)
    /// </summary>
    public class RunwayVisualRange
    {
        /// <summary>
        /// Number of the current runway
        /// </summary>
        public string RunwayNumber { get; init; }

        /// <summary>
        /// RVR value in meters/feets (min)
        /// </summary>
        public int VisibilityValue { get; init; }

        /// <summary>
        /// Max RVR value in meters/feets
        /// </summary>
        public int VisibilityValueMax { get; init; }

        /// <summary>
        /// Unit type of the current RVR
        /// </summary>
        public UnitType UnitType { get; init; }

        /// <summary>
        /// Mark of the measurement area
        /// </summary>
        public MeasurableBound MeasurableBound { get; init; }

        /// <summary>
        /// Rvr trend
        /// </summary>
        public RvrTrend RvrTrend { get; init; }

        /// <summary>
        /// Default
        /// </summary>
        public RunwayVisualRange() { }

        /// <summary>
        /// Parser constructor
        /// </summary>
        /// <param name="tokens">Array of tokens</param>
        /// <param name="errors">List of parse errors</param>
        internal RunwayVisualRange(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array of prevailing visibility tokens is empty");
                return;
            }

            var rvrRaw = tokens.First();

            UnitType = rvrRaw.Contains("FT")
                ? UnitType.Feets
                : UnitType.Meters;

            var lastLetter = rvrRaw.Substring(rvrRaw.Length - 1);
            if (Regex.IsMatch(lastLetter, @"^(U|D|N)$"))
            {
                RvrTrend = EnumTranslator.GetValueFromDescription<RvrTrend>(lastLetter);
                rvrRaw = rvrRaw[..^1];
            }

            var splittedRvr = rvrRaw.Split('/');

            RunwayNumber = splittedRvr[0][1..];

            var values = splittedRvr[1];
            values = values.Replace("FT", "");
            if (Regex.IsMatch(values[..1], @"^(M|P)$"))
            {
                MeasurableBound = EnumTranslator.GetValueFromDescription<MeasurableBound>(values[..1]);
                values = values[1..];
            }

            if (values.Contains("V"))
            {
                var splittedValues = values.Split('V');
                if (splittedValues.Length < 2)
                {
                    errors.Add($"Unexpected token {values} in {rvrRaw}");
                    return;
                }

                VisibilityValue = int.Parse(splittedValues[0]);
                VisibilityValueMax = int.Parse(splittedValues[1]);
            }
            else
                VisibilityValue = int.Parse(values);
        }
    }
}
