namespace MetarParserCore.Enums
{
    /// <summary>
    /// States of reading raw METAR
    /// NOTE: Also uses as a parse stages using integer numbers
    /// </summary>
    public enum TokenType
    {
        None = 0,

        Airport = 1,

        ObservationDayTime = 2,

        Modifier = 3,

        SurfaceWind = 4,

        PrevailingVisibility = 5,

        RunwayVisualRange = 6,

        PresentWeather = 7,

        CloudLayers = 8,

        Cavok = 9,

        Temperature = 10,

        AltimeterSetting = 11,

        RecentWeather = 12,

        WindShear = 13,

        Motne = 14,

        Trend = 15,

        Remarks = 16
    }
}
