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
#### Base types

| Classes                  | Description                                                                                   |
|--------------------------|-----------------------------------------------------------------------------------------------|
| [ReportBase](#reportBase)               | Base abstract class for all meteorological reports.                                           |
| [Metar](#metar)                    | General METAR data class. Any property of this class could be null. Inhereted from ReportBase.|

#### <a name="reportBase"/>Structure of ReportBase

| Property name        | Type                 | Description                                  |
|----------------------|----------------------|----------------------------------------------|
| ReportType           | enum [ReportType](#reportType)      | METAR report type                            |
| IsNil                | bool                 | Report is empty                              |
| Month                | enum [Month](#month)           | Current month                                |
| Modifier             | enum [MetarModifier](#metarModifier)   | METAR modifier                               |
| SurfaceWind          | [SurfaceWind](#surfaceWind)          | Info about surface wind                      |
| PrevailingVisibility | [PrevailingVisibility](#prevailingVisibility) | Info about visibility                        |
| PresentWeather       | [WeatherPhenomena](#weatherPhenomena)\[\] | Special weather conditions                   |
| CloudLayers          | [CloudLayer](#cloudLayer)\[\]       | Info about clouds (Cloud layers)             |
| ParseErrors          | string\[\]           | Array of parse errors                        |
| Unrecognized         | string\[\]           | Unrecognized tokens by METAR TokenRecognizer |

#### <a name="metar"/>Structure of Metar

| Property name      | Type                  | Description                                           |
|--------------------|-----------------------|-------------------------------------------------------|
| Airport            | string                | Airport ICAO code                                     |
| ObservationDayTime | [ObservationDayTime](#observationDayTime)    | Date and time by Zulu of the observation              |
| RunwayVisualRanges | [RunwayVisualRange](#runwayVisualRange)\[\] | Info about visibility on runways (RVR)                |
| Temperature        | [TemperatureInfo](#temperatureInfo)       | Information about temperature                         |
| AltimeterSetting   | [AltimeterSetting](#altimeterSetting)      | Information about air pressure                        |
| RecentWeather      | [WeatherPhenomena](#weatherPhenomena)      | Recent weather info                                   |
| WindShear          | [WindShear](#windShear)             | Wind shear info                                       |
| Motne              | [Motne](#motne)\[\]             | Info about runway conditions                          |
| SeaCondition       | [SeaCondition](#seaCondition)          | Info about sea-surface temperature and state          |
| Trends             | [Trend](#trend)\[\]             | Information about changes of weather forecast         |
| IsDeneb            | bool                  | Fog dispersal operations are in progress              |
| MilitaryWeather    | [MilitaryWeather](#militaryWeather)       | Military airfield weather (represents in color codes) |
| Remarks            | string                | Additional remarks (RMK)                              |

#### <a name="surfaceWind"/>Structure of SurfaceWind

| Property name         | Type                  | Description                               |
|-----------------------|-----------------------|-------------------------------------------|
| Direction             | int                   | Current wind direction (heading)          |
| IsVariable            | bool                  | Sign if wind has variable direction (VRB) |
| Speed                 | int                   | Speed of the wind                         |
| GustSpeed             | int                   | Max wind speed or gust speed              |
| WindUnit              | enum [WindUnit](#windUnit)         | Type of wind unit                         |
| ExtremeWindDirections | [ExtremeWindDirections](#extremeWindDirections) | Info about extreme wind directions        |

#### <a name="prevailingVisibility"/>Structure of PrevailingVisibility

| Property name            | Type                     | Description                                                         |
|--------------------------|--------------------------|---------------------------------------------------------------------|
| IsCavok                  | bool                     | Sign if visibility marked as CAVOK. Means Ceiling and Visibility OK |
| VisibilityInMeters       | [VisibilityInMeters](#visibilityInMeters)       | Prevailing visibility in meters                                     |
| VisibilityInStatuteMiles | [VisibilityInStatuteMiles](#visibilityInStatuteMiles) | Prevailing visibility in statute miles                              |

#### <a name="weatherPhenomena"/>Structure of WeatherPhenomena

| Property name    | Type                  | Description                         |
|------------------|-----------------------|-------------------------------------|
| WeatherCondition | enum [WeatherCondition](#weatherCondition) | Ordered array of weather conditions |

#### <a name="cloudLayer"/>Structure of CloudLayer

| Property name       | Type                     | Description                                |
|---------------------|--------------------------|--------------------------------------------|
| CloudType           | enum [CloudType](#cloudType)           | Cloud type                                 |
| CloudAltitude       | int                      | Cloud altitude                             |
| ConvectiveCloudType | enum [ConvectiveCloudType](#convectiveCloudType) | Convective cloud type                      |
| IsCloudBelow        | bool                     | Cloud below airport (in mountain airports) |

#### <a name="observationDayTime"/>Structure of ObservationDayTime

| Property name | Type | Description              |
|---------------|------|--------------------------|
| Day           | int  | Day of the current month |
| Time          | [Time](#time) | Time of the observation  |

#### <a name="runwayVisualRange"/>Structure of RunwayVisualRange

| Property name   | Type                 | Description                     |
|-----------------|----------------------|---------------------------------|
| RunwayNumber    | string               | Number of the current runway    |
| VisibilityValue | int                  | RVR value in meters/feets (min) |
| UnitType        | enum [RvrUnitType](#rvrUnitType)     | Unit type of the current RVR    |
| MeasurableBound | enum [MeasurableBound](#measurableBound) | Mark of the measurement area    |
| RvrTrend        | enum [RvrTrend](#rvrTrend)        | Rvr trend                       |

#### <a name="temperatureInfo"/>Structure of TemperatureInfo

| Property name | Type | Description                      |
|---------------|------|----------------------------------|
| Value         | int  | Temperature value in Celsius     |
| DewPoint      | int  | Temperature dew point in Celsius |

#### <a name="altimeterSetting"/>Structure of AltimeterSetting

| Property name | Type                   | Description         |
|---------------|------------------------|---------------------|
| UnitType      | enum [AltimeterUnitType](#altimeterUnitType) | Altimeter unit type |
| Value         | int                    | Altimeter value     |

#### <a name="windShear"/>Structure of WindShear

| Property name | Type               | Description               |
|---------------|--------------------|---------------------------|
| IsAll         | bool               | Wind shear on all runways |
| Type          | enum [WindShearType](#windShearType) | Wind shear type           |
| RunwayNumber  | string             | Runway number             |

#### <a name="motne"/>Structure of Motne

| Property name         | Type                            | Description                                                             |
|-----------------------|---------------------------------|-------------------------------------------------------------------------|
| RunwayNumber          | string                          | Runway number                                                           |
| Specials              | enum [MotneSpecials](#motneSpecials)              | MOTNE special sign                                                      |
| TypeOfDeposit         | enum [MotneTypeOfDeposit](#motneTypeOfDeposit)         | Type of deposit                                                         |
| ExtentOfContamination | enum [MotneExtentOfContamination](#motneExtentOfContamination) | Extent of contamination of the current runway                           |
| DepthOfDeposit        | int                             | Depth of deposit (2 digits) -1 - depth not significant (has value "//") |
| FrictionCoefficient   | int                             | Braking conditions -1 - not measured (has value "//")                   |

#### <a name="seaCondition"/>Structure of SeaCondition

| Property name  | Type          | Description                               |
|----------------|---------------|-------------------------------------------|
| SeaTemperature | int           | Temperature in Celsius                    |
| WaveHeight     | int           | Average height of the waves in decimeters |
| SeaState       | enum [SeaState](#seaState) | Sea state                                 |

#### <a name="trend"/>Structure of Trend

| Property name   | Type            | Description                                           |
|-----------------|-----------------|-------------------------------------------------------|
| TrendType       | enum [TrendType](#trendType)  | TREND report type                                     |
| TrendTime       | [TrendTime](#trendTime)       | TREND time                                            |
| MilitaryWeather | [MilitaryWeather](#militaryWeather) | Military airfield weather (represents in color codes) |

#### <a name="militaryWeather"/>Structure of MilitaryWeather

| Property name     | Type                       | Description                                        |
|-------------------|----------------------------|----------------------------------------------------|
| MilitaryColorCode | enum [MilitaryColorCode](#militaryColorCode)\[\] | Array of color codes                               |
| IsClosed          | bool                       | Sign if airfield is closed. BLACK color is defined |

#### <a name="extremeWindDirections"/>Structure of ExtremeWindDirections

| Property name            | Type | Description                                        |
|--------------------------|------|----------------------------------------------------|
| FirstExtremeDirection    | int  | First value of the extreme wind direction interval |
| LastExtremeWindDirection | int  | Last value of the extreme wind direction interval  |

#### <a name="visibilityInMeters"/>Structure of VisibilityInMeters

| Property name          | Type                     | Description                             |
|------------------------|--------------------------|-----------------------------------------|
| VisibilityValue        | int                      | Visibility value in meters              |
| VisibilityDirection    | enum [VisibilityDirection](#visibilityDirection) | Direction of the represented visibility |
| MaxVisibilityValue     | int                      | Max visibility value                    |
| MaxVisibilityDirection | enum [VisibilityDirection](#visibilityDirection) | Max visibility direction                |

#### <a name="visibilityInStatuteMiles"/>Structure of VisibilityInStatuteMiles

| Property name | Type | Description                                                     |
|---------------|------|-----------------------------------------------------------------|
| LessThanSign  | bool | (M sign) - denotes less than represented value                  |
| WholeNumber   | int  | Whole number miles of visibility/whole number of mixed fraction |
| Numerator     | int  | Numerator of the fraction                                       |
| Denominator   | int  | Denominator of the fraction                                     |

#### <a name="time"/>Structure of Time

| Property name | Type | Description |
|---------------|------|-------------|
| Hours         | int  | Hours       |
| Minutes       | int  | Minutes     |

#### <a name="trendTime"/>Enum TrendTime

| Property name | Type | Description    |
|---------------|------|----------------|
| AtTime        | [Time](#time) | Attribute "AT" |
| FromTime      | [Time](#time) | Attribute "FM" |
| TillTime      | [Time](#time) | Attribute "TL" |

# Base enums

#### <a name="reportType"/>Enum ReportType

| ValueName    | ID | Description  |
|--------------|----|--------------|
| NotSpecified | 0  |              |
| Metar        | 1  | METAR report |
| Trend        | 2  | TREND Report |

#### <a name="month"/>Enum Month

| ValueName | ID |
|-----------|----|
| None      | 0  |
| January   | 1  |
| February  | 2  |
| March     | 3  |
| April     | 4  |
| May       | 5  |
| June      | 6  |
| July      | 7  |
| August    | 8  |
| September | 9  |
| October   | 10 |
| November  | 11 |
| December  | 12 |

#### <a name="metarModifier"/>Enum MetarModifier

| ValueName | ID |
|-----------|----|
| None      | 0  |
| Auto      | 1  |
| Cor       | 2  |

#### <a name="windUnit"/>Enum WindUnit

| ValueName         | ID |
|-------------------|----|
| None              | 0  |
| MetersPerSecond   | 1  |
| KilometersPerHour | 2  |
| Knots             | 3  |

#### <a name="weatherCondition"/>Enum WeatherCondition

| ValueName            | ID | Description                               |
|----------------------|----|-------------------------------------------|
| None                 | 0  |                                           |
| Light                | 1  |                                           |
| Heavy                | 2  |                                           |
| Vicinity             | 3  |                                           |
| Shallow              | 4  |                                           |
| Patches              | 5  |                                           |
| Partial              | 6  |                                           |
| Drifting             | 7  |                                           |
| Blowing              | 8  |                                           |
| Shower               | 9  |                                           |
| Thunderstorm         | 10 |                                           |
| Freezing             | 11 |                                           |
| Drizzle              | 12 |                                           |
| Rain                 | 13 |                                           |
| Snow                 | 14 |                                           |
| SnowGrains           | 15 |                                           |
| IceCrystals          | 16 |                                           |
| IcePellets           | 17 |                                           |
| Hail                 | 18 |                                           |
| SnowPellets          | 19 |                                           |
| UnknownPrecipitation | 20 |                                           |
| Mist                 | 21 | Mist - visibility is 1000 meters or more  |
| Fog                  | 22 | Fog - visibility is less than 1000 meters |
| Smoke                | 23 |                                           |
| VolcanicAsh          | 24 |                                           |
| Dust                 | 25 |                                           |
| Sand                 | 26 |                                           |
| Haze                 | 27 |                                           |
| Spray                | 28 |                                           |
| DustWhirls           | 29 |                                           |
| Squalls              | 30 |                                           |
| FunnelCloud          | 31 |                                           |
| SandStorm            | 32 |                                           |
| DustStorm            | 33 |                                           |
| NoSignificantWeather | 34 | NSW                                       |

#### <a name="cloudType"/>Enum CloudType

| ValueName           | ID | Description                                                  |
|---------------------|----|--------------------------------------------------------------|
| None                | 0  | Not specified                                                |
| SkyClear            | 1  | Sky clear - No cloud present                                 |
| Few                 | 2  | Few - 1-2 oktas                                              |
| Scattered           | 3  | Scattered - 3-4 oktas                                        |
| Broken              | 4  | Broken - 5-7 oktas                                           |
| Overcast            | 5  | Overcast - 8 oktas                                           |
| VerticalVisibility  | 6  | Vertical visibility - indefinite ceiling                     |
| Clear               | 7  | Clear below 10,000 ft as interpreted by an autostation       |
| NoSignificantClouds | 8  | No significant clouds - clouds present at and above 5,000 ft |
| NoCloudDetected     | 9  | No cloud detected                                            |

#### <a name="convectiveCloudType"/>Enum ConvectiveCloudType

| ValueName       | ID |
|-----------------|----|
| None            | 0  |
| Cumulonimbus    | 1  |
| ToweringCumulus | 2  |

#### <a name="rvrUnitType"/>Enum RvrUnitType

| ValueName | ID |
|-----------|----|
| None      | 0  |
| Meters    | 1  |
| Feets     | 2  |

#### <a name="measurableBound"/>Enum MeasurableBound

| ValueName | ID | Description                            |
|-----------|----|----------------------------------------|
| None      | 0  |                                        |
| Lower     | 1  | Preceding the lowest measurable value  |
| Higher    | 2  | Preceding the highest measurable value |

#### <a name="rvrTrend"/>Enum RvrTrend

| ValueName | ID | Description              |
|-----------|----|--------------------------|
| None      | 0  | Not represented          |
| Upward    | 1  | Visibility became better |
| Downward  | 2  | Visibility became worse  |
| NoChange  | 3  | Without changes          |

#### <a name="altimeterUnitType"/>Enum AltimeterUnitType

| ValueName       | ID |
|-----------------|----|
| None            | 0  |
| Hectopascal     | 1  |
| InchesOfMercury | 2  |

#### <a name="windShearType"/>Enum WindShearType

| ValueName | ID | Description                           |
|-----------|----|---------------------------------------|
| Both      | 0  | Wind shear during landing or take off |
| TakeOff   | 1  | Wind shear during take off            |
| Landing   | 2  | Wind shear during landing             |

#### <a name="motneSpecials"/>Enum MotneSpecials

| ValueName    | ID | Description                                              |
|--------------|----|----------------------------------------------------------|
| Default      | 0  | Special sign not specified                               |
| Cleared      | 1  | Contamination has disappeared or runway has been cleared |
| Closed       | 2  | Runway closed                                            |
| ClosedToSnow | 3  | Closed due to snow                                       |

#### <a name="motneTypeOfDeposit"/>Enum MotneTypeOfDeposit

| ValueName   | ID |
|-------------|----|
| ClearAndDry | 0  |
| Damp        | 1  |
| Wet         | 2  |
| Rime        | 3  |
| DrySnow     | 4  |
| WetSnow     | 5  |
| Slush       | 6  |
| Ice         | 7  |
| RolledSnow  | 8  |
| FrozenRuts  | 9  |
| NotReported | 10 |

#### <a name="motneExtentOfContamination"/>Enum MotneExtentOfContamination

| ValueName   | ID | Description                   |
|-------------|----|-------------------------------|
| NotReported | 0  | Not reported, marked as "/"   |
| Less10      | 1  | 10% or less of runway covered |
| From11To25  | 2  | 11% to 25% of runway covered  |
| From26To50  | 3  | 26% to 50% of runway covered  |
| From51To100 | 4  | 51% to 100% of runway covered |

#### <a name="seaState"/>Enum SeaState

| ValueName  | ID | Description                          |
|------------|----|--------------------------------------|
| Glassy     | 0  | Waves height = 0 meters              |
| Rippled    | 1  | Waves height from 0 to 0.1 meters    |
| Wavelets   | 2  | Waves height from 0.1 to 0.5 meters  |
| Slight     | 3  | Waves height from 0.5 to 1.25 meters |
| Moderate   | 4  | Waves height from 1.25 to 2.5 meters |
| Rough      | 5  | Waves height from 2.5 to 4 meters    |
| VeryRough  | 6  | Waves height from 4 to 6 meters      |
| High       | 7  | Wave height from 6 to 9 meters       |
| VeryHigh   | 8  | Wave height from 9 to 14 meters      |
| Phenomenal | 9  | Wave height over 14 meters           |
| None       | 10 | Not reported                         |

#### <a name="trendType"/>Enum TrendType

| ValueName            | ID | Description                  |
|----------------------|----|------------------------------|
| None                 | 0  | Not specified                |
| Becoming             | 1  | A changes may happen or not  |
| Tempo                | 2  | Changes is definitely happen |
| NoSignificantChanges | 3  |                              |

#### <a name="militaryColorCode"/>Enum MilitaryColorCode

| ValueName | ID | Description                                            |
|-----------|----|--------------------------------------------------------|
| Unknown   | 0  | Unknown code                                           |
| Blue      | 1  | Visibility is greater 5 mi, ceiling is greater 2500 ft |
| White     | 2  | Visibility 3 3/8 - 5 mi, ceiling 1500 - 2500 ft        |
| Green     | 3  | Visibility 2 1/4 - 3 - 1/8, ceiling > 700 - 1500 ft    |
| Yellow    | 4  | Visibility 1 1/8 - 2 - 1/4, ceiling > 300 - 700 ft     |
| Amber     | 5  | Visibility 1/2 - 1 1/8 mi, ceiling 200 - 300 ft        |
| Red       | 6  | Visibility less 1/2 mi, ceiling less 200 ft            |

#### <a name="visibilityDirection"/>Enum VisibilityDirection

| ValueName | ID |
|-----------|----|
| NotSet    | 0  |
| North     | 1  |
| NorthEast | 2  |
| East      | 3  |
| SouthEast | 4  |
| South     | 5  |
| SouthWest | 6  |
| West      | 7  |
| NorthWest | 8  |

# Feedback

If you have any feedback, contact me valerxenus@gmail.com
