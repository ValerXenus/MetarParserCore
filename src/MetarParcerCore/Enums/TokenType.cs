namespace MetarParserCore.Enums
{
    /// <summary>
    /// States of reading raw METAR
    /// </summary>
    internal enum TokenType
    {
        Unknown = 0,

        Airport = 1,

        ObservationDayTime = 2,

        Modifier = 3,

        SurfaceWind = 4,

        PrevailingVisibility = 5,

        RunwayVisualRange = 6,

        PresentWeather = 7,

        CloudLayers = 8,

        Temperature = 9,

        AltimeterSetting = 10,

        RecentWeather = 11,

        WindShear = 12,

        Motne = 13,

        Trend = 14,

        Remarks = 15,

        Special = 99
    }
}
