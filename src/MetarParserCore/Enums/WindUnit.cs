using System.ComponentModel;

namespace MetarParserCore.Enums
{
    /// <summary>
    /// Wind unit types
    /// </summary>
    public enum WindUnit
    {
        [Description("MPS")]
        MetersPerSecond = 0,

        [Description("KMT")]
        KilometersPerHour = 1,

        [Description("KT")]
        Knots = 3
    }
}
