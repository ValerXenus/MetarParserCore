# MetarParserCore
A .NET 6.0 library intended for parsing raw METAR data. Current version can parse METAR and TREND reports.
Nuget package: https://www.nuget.org/packages/MetarParserCore/

# Getting started
This library is easy to use. Just follow exanple below:

```cs
// Input raw METAR string
var raw = "UWKD 291500Z 32003MPS CAVOK 18/02 Q1019 R29/CLRD70 NOSIG RMK QFE753/1004=";
// Initialize METAR parser
var metarParser = new MetarParser();
// Parse raw METAR
var airportMetar = metarParser.Parse(raw);
// Serialization using Newtonsoft.Json
var serializedResult = JsonConvert.SerializeObject(airportMetar);
```

# Types overview
#### All base types

| Classes                  | Description                                                                                                                                                              |
|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Metar](#metar)                    | General METAR data class. Any property of this class could be null. Inhereted from ReportBase.                                                                           |
| ReportBase               | Base abstract class for all meteorological reports.                                                                                                                      |
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
| Trend                    | Information about changes of weather forecast (TREND). Inhereted from ReportBase.                                                                                        |
| ExtremeWindDirections    | Info about two extreme wind directions during the 10 minute period of the observation.                                                                                   |
| Time                     | Custom time class.                                                                                                                                                       |
| TrendTime                | Info about trend times. Composed of AtTime (AT), FromTime (FM) and TillTime (TL).                                                                                        |
| VisibilityInMeters       | Prevailing visibility in meters.                                                                                                                                         |
| VisibilityInStatuteMiles | Prevailing visibility measuring in statute miles.                                                                                                                        |
| MetarParser              | General METAR parser class.                                                                                                                                              |

#### <a name="metar"/>Structure of Metar

| Property name      | Type                  | Description                                           |
|--------------------|-----------------------|-------------------------------------------------------|
| Airport            | string                | Airport ICAO code                                     |
| ObservationDayTime | ObservationDayTime    | Date and time by Zulu of the observation              |
| RunwayVisualRanges | RunwayVisualRange\[\] | Info about visibility on runways (RVR)                |
| Temperature        | TemperatureInfo       | Information about temperature                         |
| AltimeterSetting   | AltimeterSetting      | Information about air pressure                        |
| RecentWeather      | WeatherPhenomena      | Recent weather info                                   |
| WindShear          | WindShear             | Wind shear info                                       |
| Motne              | Motne\[\]             | Info about runway conditions                          |
| SeaCondition       | SeaCondition          | Info about sea-surface temperature and state          |
| Trends             | Trend\[\]             | Information about changes of weather forecast         |
| IsDeneb            | bool                  | Fog dispersal operations are in progress              |
| MilitaryWeather    | MilitaryWeather       | Military airfield weather (represents in color codes) |
| Remarks            | string                | Additional remarks (RMK)                              |




# Feedback

If you have any feedback, contact me valerxenus@gmail.com
