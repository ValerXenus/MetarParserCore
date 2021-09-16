using MetarParcerCore.Enums;

namespace MetarParcerCore.Objects
{
    /// <summary>
    /// General METAR data class
    /// </summary>
    public class Metar
    {
        /// <summary>
        /// Airport ICAO code
        /// </summary>
        public string Airport { get; init; }

        /// <summary>
        /// Date and time by Zulu
        /// </summary>
        public ZuluDateTime ZuluDateTime { get; init; }

        /// <summary>
        /// METAR modifier
        /// </summary>
        public MetarModifier Modifier { get; init; }

        /// <summary>
        /// Info about wind
        /// </summary>
        public WindInfo Wind { get; init; }

        /// <summary>
        /// Info about visibility
        /// </summary>
        public VisibilityInfo Visibility { get; init; }

        /// <summary>
        /// Info about runway visibility
        /// </summary>
        public RunwayVisibilityInfo RunwayVisibility { get; init; }

        /// <summary>
        /// Special weather conditions
        /// </summary>
        public SpecialConditions SpecialConditions { get; init; }

        /// <summary>
        /// Info about clouds
        /// </summary>
        public CloudsInfo Clouds { get; init; }

        /// <summary>
        /// Identifier of favorable weather
        /// </summary>
        public bool IsCavok { get; init; }

        /// <summary>
        /// Information about temperature
        /// </summary>
        public TemperatureInfo Temperature { get; init; }

        /// <summary>
        /// Information about air pressure
        /// </summary>
        public AirPressureInfo AirPressure { get; init; }

        /// <summary>
        /// Additional information
        /// </summary>
        public AdditionalInformation AdditionalInformation { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        public WeatherForecastChanges ForecastChanges { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        public RunwayConditions RunwayConditions { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        public string Remarks { get; init; }
    }
}
