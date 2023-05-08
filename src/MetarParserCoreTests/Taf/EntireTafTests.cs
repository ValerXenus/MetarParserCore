using MetarParserCore.Enums;
using MetarParserCore.Objects.Supplements;
using MetarParserCore.Objects;
using MetarParserCore;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests.Taf
{
    public class EntireTafTests
    {
        [Fact]
        public void ParseMetarExampleUwkd_Successful()
        {
            var rawString = "TAF UWKD 041353Z 0415/0515 34003MPS 9999 SCT020 TX23/0511Z TN07/0502Z" +
                            "\r\nBECMG 0507/0509 20005G12MPS" +
                            "\r\nTEMPO 0511/0515 35005MPS 6000 -SHRA BKN016 BKN020CB";
            var tafParser = new TafParser();
            var airportTaf = tafParser.Parse(rawString);

            Assert.Null(airportTaf.ParseErrors);

            #region Valid object

            var validTaf = new Metar
            {
                ReportType = ReportType.Metar,
                Airport = "UWKD",
                ObservationDayTime = new ObservationDayTime
                {
                    Day = 2,
                    Time = new Time
                    {
                        Hours = 13,
                        Minutes = 30
                    }
                },
                SurfaceWind = new SurfaceWind
                {
                    Direction = 170,
                    Speed = 7,
                    GustSpeed = 12,
                    WindUnit = WindUnit.MetersPerSecond
                },
                PrevailingVisibility = new PrevailingVisibility
                {
                    VisibilityInMeters = new VisibilityInMeters
                    {
                        VisibilityValue = 5000,
                        MaxVisibilityValue = 6500,
                        MaxVisibilityDirection = VisibilityDirection.SouthEast
                    }
                },
                RunwayVisualRanges = new RunwayVisualRange[]
                {
                    new RunwayVisualRange
                    {
                        RunwayNumber = "29",
                        VisibilityValue = 1500,
                        UnitType = RvrUnitType.Meters,
                        RvrTrend = RvrTrend.Downward
                    }
                },
                PresentWeather = new WeatherPhenomena[]
                {
                    new WeatherPhenomena
                    {
                        WeatherConditions = new WeatherCondition[]
                        {
                            WeatherCondition.Shower,
                            WeatherCondition.Rain
                        }
                    }
                },
                CloudLayers = new CloudLayer[]
                {
                    new CloudLayer()
                    {
                        CloudType = CloudType.Broken,
                        Altitude = 15,
                        ConvectiveCloudType = ConvectiveCloudType.Cumulonimbus
                    }
                },
                Temperature = new TemperatureInfo
                {
                    Value = 17,
                    DewPoint = 14
                },
                AltimeterSetting = new AltimeterSetting
                {
                    Value = 1001,
                    UnitType = AltimeterUnitType.Hectopascal
                },
                WindShear = new WindShear
                {
                    Runway = "29",
                    Type = WindShearType.TakeOff
                },
                Motne = new Motne[]
                {
                    new Motne
                    {
                        RunwayNumber = "29",
                        DepthOfDeposit = 0,
                        ExtentOfContamination = MotneExtentOfContamination.NotReported,
                        FrictionCoefficient = 50,
                        Specials = MotneSpecials.Default,
                        TypeOfDeposit = MotneTypeOfDeposit.Wet
                    }
                },
                Trends = new Trend[]
                {
                    new Trend
                    {
                        ReportType = ReportType.Trend,
                        TrendType = TrendType.Becoming,
                        TrendTime = new TrendTime
                        {
                            AtTime = new Time
                            {
                                Hours = 13,
                                Minutes = 50,
                            }
                        },
                        SurfaceWind = new SurfaceWind
                        {
                            Direction = 20,
                            Speed = 20,
                            WindUnit = WindUnit.MetersPerSecond
                        }
                    },
                    new Trend
                    {
                        ReportType = ReportType.Trend,
                        TrendType = TrendType.Tempo,
                        SurfaceWind = new SurfaceWind
                        {
                            IsVariable = true,
                            Speed = 15,
                            WindUnit = WindUnit.MetersPerSecond
                        },
                        PresentWeather = new WeatherPhenomena[]
                        {
                            new WeatherPhenomena
                            {
                                WeatherConditions = new WeatherCondition[]
                                {
                                    WeatherCondition.Light,
                                    WeatherCondition.Thunderstorm,
                                    WeatherCondition.Rain
                                }
                            }
                        },
                        CloudLayers = new CloudLayer[]
                        {
                            new CloudLayer
                            {
                                CloudType = CloudType.Broken,
                                Altitude = 20,
                                ConvectiveCloudType = ConvectiveCloudType.Cumulonimbus
                            },
                            new CloudLayer
                            {
                                CloudType = CloudType.Overcast,
                                Altitude = 110
                            }
                        }
                    }
                },
                Remarks = "QFE740/0987"
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(airportTaf);
            var validResults = JsonConvert.SerializeObject(validTaf);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void ParseTafExampleNil_Successful()
        {
            const string validAirport = "UWKD";

            var rawString = "TAF UWKD 020500Z NIL=";
            var tafParser = new TafParser();
            var airportTaf = tafParser.Parse(rawString);

            Assert.Null(airportTaf.ParseErrors);
            Assert.Equal(validAirport, airportTaf.Airport);
            Assert.Equal(true, airportTaf.IsNil);
        }

        [Fact]
        public void ParseTafExampleNCnl_Successful()
        {
            var rawString = "TAF UWKD 011100Z 0312/0418 CNL";
            var tafParser = new TafParser();
            var airportTaf = tafParser.Parse(rawString);

            #region Valid object

            var validTaf = new MetarParserCore.Objects.Taf
            {
                ReportType = ReportType.Taf,
                Airport = "UWKD",
                ObservationDayTime = new ObservationDayTime
                {
                    Day = 1,
                    Time = new Time
                    {
                        Hours = 11,
                        Minutes = 0
                    }
                },
                TafForecastPeriod = new TafForecastPeriod
                {
                    Start = new TafDayTime { Day = 3, Hours = 12 },
                    End = new TafDayTime { Day = 4, Hours = 18 }
                },
                IsReportCancelled = true
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(airportTaf);
            var validResults = JsonConvert.SerializeObject(validTaf);
            Assert.Null(airportTaf.ParseErrors);
            Assert.Equal(validResults, parseResults);
        }
    }
}