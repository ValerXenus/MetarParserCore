using MetarParserCore.Enums;

namespace MetarParserCore.Objects
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
        /// Date and time by Zulu of the observation
        /// </summary>
        public ObservationDayTime ObservationDayTime { get; init; }

        /// <summary>
        /// Current month
        /// </summary>
        public Month Month { get; init; }

        /// <summary>
        /// METAR modifier
        /// </summary>
        public MetarModifier Modifier { get; init; }

        /// <summary>
        /// Info about surface wind
        /// </summary>
        public SurfaceWind SurfaceWind { get; init; }

        /// <summary>
        /// Info about visibility
        /// </summary>
        public PrevailingVisibility PrevailingVisibility { get; init; }

        /// <summary>
        /// Info about runway visibility (RVR)
        /// </summary>
        public RunwayVisualRange RunwayVisualRange { get; init; }

        /// <summary>
        /// Special weather conditions
        /// </summary>
        public PresentWeather PresentWeather { get; init; }

        /// <summary>
        /// Info about clouds (Cloud layers)
        /// </summary>
        public CloudLayers CloudLayers { get; init; }

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
        public AltimeterSetting AltimeterSetting { get; init; }

        /// <summary>
        /// Recent weather info
        /// </summary>
        public RecentWeather RecentWeather { get; init; }

        /// <summary>
        /// Wind shear info
        /// </summary>
        public string[] WindShear { get; init; }

        /// <summary>
        /// Info about runway conditions
        /// </summary>
        public Motne Motne { get; init; }

        /// <summary>
        /// Information about changes of weather forecast
        /// </summary>
        public Trend Trend { get; init; }

        /// <summary>
        /// Additional remarks (RMK)
        /// </summary>
        public string Remarks { get; init; }

        /// <summary>
        /// Set of parse errors
        /// </summary>
        public string[] ParseErrors { get; init; }
    }
}
