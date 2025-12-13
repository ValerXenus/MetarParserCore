using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MetarParserCore.Enums;
using MetarParserCore.Extensions;

namespace MetarParserCore.Objects
{
    /// <summary>
    /// Information about air pressure
    /// </summary>
    [DataContract(Name = "altimeterSetting")]
    public class AltimeterSetting
    {
        /// <summary>
        /// Altimeter unit type
        /// </summary>
        [DataMember(Name = "unitType", EmitDefaultValue = false)]
        public AltimeterUnitType UnitType { get; init; }

        /// <summary>
        /// Altimeter value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public int Value { get; init; }
        
        /// <summary>
        /// Is true when source metar has unknown pressure value.
        /// Example: Q////
        /// </summary>
        public bool IsUnknown { get; set; }

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

            UnitType = EnumTranslator.GetValueByDescription<AltimeterUnitType>(altimeterToken[..1]);
            
            if (altimeterToken[1..].StartsWith("////"))
            {
                IsUnknown = true;
            }
            else
            {
                Value = int.Parse(altimeterToken[1..]);
                IsUnknown = false;
            }
        }

        #endregion
    }
}
