namespace MetarParserCore.Enums
{
    /// <summary>
    /// States of reading raw METAR
    /// Also uses as a parse stages using integer numbers
    /// </summary>
    public enum TokenType
    {
        None = 0,

        Airport = 1,

        ObservationDayTime = 2,

        Modifier = 3,

        SurfaceWind = 4,

        HorizontalVisibility = 5,

        RunwayVisibility = 6,

        SpecialCondition = 7,

        CloudLayers = 8,

        Cavok = 9,

        Temperature = 10,

        AirPressure = 11,

        AdditionalInformation = 12,

        ForecastChange = 13,

        RunwayCondition = 14,

        Remarks = 15
    }
}
