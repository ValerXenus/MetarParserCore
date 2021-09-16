﻿using System.ComponentModel;

namespace MetarParcerCore.Enums
{
    /// <summary>
    /// Types of METAR data
    /// </summary>
    public enum MetarModifier
    {
        [Description("None")]
        None = 0,

        [Description("AUTO")]
        Auto = 1,

        [Description("COR")]
        Cor = 2
    }
}
