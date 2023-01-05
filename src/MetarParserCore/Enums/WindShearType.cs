using System.ComponentModel;

namespace MetarParserCore.Enums
{
    /// <summary>
    /// Wind shear types on runway
    /// </summary>
    public enum WindShearType
    {
        None = 0,

        /// <summary>
        /// Wind shear during landing or take off
        /// </summary>
        Both = 1,

        /// <summary>
        /// Wind shear during take off
        /// </summary>
        [Description("TKOF")]
        TakeOff = 2,

        /// <summary>
        /// Wind shear during landing 
        /// </summary>
        [Description("LDG")]
        Landing = 3,
    }
}
