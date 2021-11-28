# MetarParserCore
A .NET 5.0 library intended for parsing raw METAR data. Current version can parse METAR and TREND reports.

# Getting started
This library is easy to use. Just follow exanple below:

```cs
// Input raw METAR string
var raw = "UWKD 291500Z 32003MPS CAVOK 18/02 Q1019 R29/CLRD70 NOSIG RMK QFE753/1004=";
// Initialize METAR parser
var metarParser = new MetarParser();
// Parse raw METAR
var airportMetar = metarParser.Parse(raw);
```

# Classes overview

| Classes                  | Description                                                                                                                                                              |
|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Metar                    | General METAR data class. Any property of this class could be null. Inhereted from ReportBase.                                                                           |
| AltimeterSetting         | Information about air pressure.                                                                                                                                          |
| CloudLayer               | Info about clouds and vertical visibility (Cloud layers).                                                                                                                |
| MilitaryWeather          | Weather info on military airfields (Military color codes).                                                                                                               |
| Motne                    | Info about runway conditions.                                                                                                                                            |
| ObservationDayTime       | Date and time of the airport by Zulu.                                                                                                                                    |
| PrevailingVisibility     | Horizontal visibility at the surface of the earth. Composed of VisibilityInMeters and VisibilityInStatuteMiles. Depends on unit only one of these members can be filled. |
| RunwayVisualRange        | Info about visibility on the runway (RVR).                                                                                                                               |
| SeaCondition             | Info about sea-surface temperature and state.                                                                                                                            |
| SurfaceWind              | Surface wind information.                                                                                                                                                |
| TemperatureInfo          | Information about air temperature and dew point.                                                                                                                         |
| WeatherPhenomena         | Special weather conditions.                                                                                                                                              |
| WindShear                | Info about windshear on runways.                                                                                                                                         |
| ReportBase               | Base abstract class of all meteorological reports.                                                                                                                       |
| Trend                    | Information about changes of weather forecast (TREND). Inhereted from ReportBase.                                                                                        |
| ExtremeWindDirections    | Info about two extreme wind directions during the 10 minute period of the observation.                                                                                   |
| Time                     | Custom time class.                                                                                                                                                       |
| TrendTime                | Info about trend times. Composed of AtTime (AT), FromTime (FM) and TillTime (TL).                                                                                        |
| VisibilityInMeters       | Prevailing visibility in meters.                                                                                                                                         |
| VisibilityInStatuteMiles | Prevailing visibility measuring in statute miles.                                                                                                                        |
| MetarParser              | General METAR parser class.                                                                                                                                              |

# Feedback

If you have any feedback, contact me valeraxenus@mail.ru