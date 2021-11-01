using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Information about air pressure
    /// </summary>
    public class AltimeterSetting
    {
        /// <summary>
        /// Altimeter unit type
        /// </summary>
        public AltimeterUnitType UnitType { get; init; }

        /// <summary>
        /// Altimeter value
        /// </summary>
        public int Value { get; init; }

        #region Constructors

        /// <summary>
        /// Default
        /// </summary>
        public AltimeterSetting() { }

        internal AltimeterSetting(string[] tokens, List<string> errors)
        {
            if (tokens.Length == 0)
            {
                errors.Add("Array with altimeter token is empty");
                return;
            }

            var altimeterToken = tokens.First();
            if (string.IsNullOrEmpty(altimeterToken))
            {
                errors.Add("Cannot parse empty altimeter token");
                return;
            }

            UnitType = EnumTranslator.GetValueByDescription<AltimeterUnitType>(altimeterToken[..1]);
            Value = int.Parse(altimeterToken[1..]);
        }

        #endregion
    }
}
