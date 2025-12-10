using MetarParserCore;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.Objects.Supplements;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class EntireMetarTests
    {
        [Fact]
        public void ParseMetarExampleUwkd_Successful()
        {
            var rawString = "UWKD 021330Z 17007G12MPS 5000 6500SE R29/1500D SHRA BKN015CB 17/14 Q1001 WS TKOF RWY29 R29/2/0050 BECMG AT1350 02020MPS TEMPO VRB15MPS -TSRA BKN020CB OVC110 RMK QFE740/0987=";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);

            #region Valid object

            var validMetar = new Metar
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

            var parseResults = JsonConvert.SerializeObject(airportMetar);
            var validResults = JsonConvert.SerializeObject(validMetar);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void ParseMetarExampleNil_Successful()
        {
            var rawString = "UWKD 020500Z NIL=";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Airport, "UWKD");
            Assert.Equal(airportMetar.IsNil, true);
        }

        [Fact]
        public void ParseMetarExampleKlax_Successful()
        {
            //var rawString = "KLAX 071753Z 27006KT 2 1/2SM HZ OVC012 16/12 A3007 RMK AO2 SLP181 VIS SW-NW 1 3/4 T01560117 10156 20133 52006";
            var rawString = "KLAX 071753Z 27006KT 2 1/2SM VCTS OVC012 16/12 A3007 RMK AO2 SLP181 VIS SW-NW 1 3/4 T01560117 10156 20133 52006";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);

            #region Valid object

            var validMetar = new Metar
            {
                ReportType = ReportType.Metar,
                Airport = "KLAX",
                ObservationDayTime = new ObservationDayTime
                {
                    Day = 7,
                    Time = new Time
                    {
                        Hours = 17,
                        Minutes = 53
                    }
                },
                SurfaceWind = new SurfaceWind
                {
                    Direction = 270,
                    Speed = 6,
                    WindUnit = WindUnit.Knots
                },
                PrevailingVisibility = new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        WholeNumber = 2,
                        Numerator = 1,
                        Denominator = 2
                    }
                },
                PresentWeather = new WeatherPhenomena[]
                {
                    new WeatherPhenomena
                    {
                        WeatherConditions = new WeatherCondition[]
                        {
                            //WeatherCondition.Haze
                            WeatherCondition.Vicinity,
                            WeatherCondition.Thunderstorm
                        }
                    }
                },
                CloudLayers = new CloudLayer[]
                {
                    new CloudLayer()
                    {
                        CloudType = CloudType.Overcast,
                        Altitude = 12
                    }
                },
                Temperature = new TemperatureInfo
                {
                    Value = 16,
                    DewPoint = 12
                },
                AltimeterSetting = new AltimeterSetting
                {
                    Value = 3007,
                    UnitType = AltimeterUnitType.InchesOfMercury
                },
                Remarks = "AO2 SLP181 VIS SW-NW 1 3/4 T01560117 10156 20133 52006"
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(airportMetar);
            var validResults = JsonConvert.SerializeObject(validMetar);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void ParseMetarExampleEgll_Successful()
        {
            var rawString = "EGLL 071750Z AUTO 25004KT 9999 NCD 09/04 Q1022 NOSIG";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);

            #region Valid object

            var validMetar = new Metar
            {
                ReportType = ReportType.Metar,
                Modifier = MetarModifier.Auto,
                Airport = "EGLL",
                ObservationDayTime = new ObservationDayTime
                {
                    Day = 7,
                    Time = new Time
                    {
                        Hours = 17,
                        Minutes = 50
                    }
                },
                SurfaceWind = new SurfaceWind
                {
                    Direction = 250,
                    Speed = 4,
                    WindUnit = WindUnit.Knots
                },
                PrevailingVisibility = new PrevailingVisibility
                {
                    VisibilityInMeters = new VisibilityInMeters
                    {
                        VisibilityValue = 9999
                    }
                },
                CloudLayers = new CloudLayer[]
                {
                    new CloudLayer()
                    {
                        CloudType = CloudType.NoCloudDetected
                    }
                },
                Temperature = new TemperatureInfo
                {
                    Value = 9,
                    DewPoint = 4
                },
                AltimeterSetting = new AltimeterSetting
                {
                    Value = 1022,
                    UnitType = AltimeterUnitType.Hectopascal
                },
                Trends = new Trend[]
                {
                    new Trend
                    {
                        ReportType = ReportType.Trend,
                        TrendType = TrendType.NoSignificantChanges
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(airportMetar);
            var validResults = JsonConvert.SerializeObject(validMetar);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void ParseMetarExampleKsme_Successful()
        {
            var rawString = "KSME 171053Z AUTO 01107KT 1 3/4SM ERROR BR VV007";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Unrecognized[0], "ERROR");

            #region Valid object

            var validMetar = new Metar
            {
                ReportType = ReportType.Metar,
                Modifier = MetarModifier.Auto,
                Airport = "KSME",
                ObservationDayTime = new ObservationDayTime
                {
                    Day = 17,
                    Time = new Time
                    {
                        Hours = 10,
                        Minutes = 53
                    }
                },
                SurfaceWind = new SurfaceWind
                {
                    Direction = 11,
                    Speed = 7,
                    WindUnit = WindUnit.Knots
                },
                PrevailingVisibility = new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        WholeNumber = 1,
                        Numerator = 3,
                        Denominator = 4
                    }
                },
                PresentWeather = new WeatherPhenomena[]
                {
                    new WeatherPhenomena
                    {
                        WeatherConditions = new WeatherCondition[]
                        {
                            WeatherCondition.Mist
                        }
                    }
                },
                CloudLayers = new CloudLayer[]
                {
                    new CloudLayer()
                    {
                        CloudType = CloudType.VerticalVisibility,
                        Altitude = 7
                    }
                },
                Unrecognized = new []
                {
                    "ERROR"
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(airportMetar);
            var validResults = JsonConvert.SerializeObject(validMetar);
            Assert.Equal(parseResults, validResults);
        }
    }
}
