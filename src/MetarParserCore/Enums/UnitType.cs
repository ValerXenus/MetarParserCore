using System.ComponentModel;

namespace MetarParserCore.Enums
{
    /// <summary>
    /// Enum of unit types
    /// </summary>
    public enum UnitType
    {
        [Description("None")]
        None = 0,

        [Description("Meters")]
        Meters = 1,

        [Description("Feets")]
        Feets = 2
    }
}
