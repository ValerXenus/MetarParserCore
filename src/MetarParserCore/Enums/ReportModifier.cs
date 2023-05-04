using System.ComponentModel;

namespace MetarParserCore.Enums
{
    /// <summary>
    /// Types of METAR data
    /// </summary>
    public enum ReportModifier
    {
        [Description("None")]
        None = 0,

        [Description("AUTO")]
        Auto = 1,

        [Description("COR")]
        Cor = 2,

        /// <summary>
        /// TAF report corrected modifier
        /// </summary>
        [Description("AMD")]
        Amd = 3,
    }
}
