namespace MetarParserCore.Enums
{
    /// <summary>
    /// Type of deposit
    /// </summary>
    public enum MotneTypeOfDeposit
    {
        None = 0,

        ClearAndDry = 1,

        Damp = 2,

        Wet = 3,

        Rime = 4,

        DrySnow = 5,

        WetSnow = 6,

        Slush = 7,

        Ice = 8,

        RolledSnow = 9,

        FrozenRuts = 10,

        /// <summary>
        /// Marked as "/"
        /// </summary>
        NotReported = 11
    }
}
