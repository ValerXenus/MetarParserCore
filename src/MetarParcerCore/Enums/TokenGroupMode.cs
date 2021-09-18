namespace MetarParserCore.Enums
{
    /// <summary>
    /// Enum of grouping modes for complex tokens
    /// </summary>
    internal enum TokenGroupMode
    {
        None = 0,

        RecentWeatherGroup = 1,

        WindShearGroup = 2,

        TrendGroup = 3,

        RemarksGroup = 4
    }
}
