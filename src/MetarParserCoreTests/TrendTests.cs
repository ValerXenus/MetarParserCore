using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.Objects.Supplements;
using MetarParserCore.TokenLogic;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class TrendTests
    {
        [Fact]
        public void TrendParser_Successful()
        {
            var trends = new[]
            {
                "BECMG AT1330 02020MPS TEMPO VRB15MPS -TSRA BKN020CB OVC110",
                "BECMG FM1330 02020MPS TEMPO VRB15MPS -TSRA BKN020CB",
                "NOSIG",
                "BECMG AT1330 02020MPS",
                "TEMPO VRB15MPS -TSRA BKN020CB OVC110",
                "TEMPO 2100 -SHRA BKN015CB",
                "BECMG AT1330 NSW",
                "TEMPO GRN YLO"
            };

            var errors = new List<string>();
            var parsedTrends = new List<Trend>();

            foreach (var trend in trends)
            {
                var splitted = trend.ToUpper().Split(" ");
                var reports = Recognizer.Instance().RecognizeAndGroupTokensTrend(splitted);

                foreach (var report in reports)
                {
                    var current = new Trend(report, Month.None);
                    if (current.ParseErrors is { Length: > 0 })
                    {
                        errors = errors.Concat(current.ParseErrors).ToList();
                        continue;
                    }

                    parsedTrends.Add(current);
                }
            }

            Assert.Equal(errors.Count, 0);
            Assert.Equal(parsedTrends.Count, 10);

            #region Valid object

            var validResultsObject = new List<Trend>
            {
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Becoming,
                    TrendTime = new TrendTime
                    {
                        AtTime = new Time { Hours = 13, Minutes = 30 }
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
                            WeatherConditions = new []
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
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Becoming,
                    TrendTime = new TrendTime
                    {
                        FromTime = new Time { Hours = 13, Minutes = 30 }
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
                            WeatherConditions = new []
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
                        }
                    }
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.NoSignificantChanges
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Becoming,
                    TrendTime = new TrendTime
                    {
                        AtTime = new Time { Hours = 13, Minutes = 30 }
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
                            WeatherConditions = new []
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
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Tempo,
                    PrevailingVisibility = new PrevailingVisibility
                    {
                        VisibilityInMeters = new VisibilityInMeters
                        {
                            VisibilityValue = 2100
                        }
                    },
                    PresentWeather = new WeatherPhenomena[]
                    {
                        new WeatherPhenomena
                        {
                            WeatherConditions = new []
                            {
                                WeatherCondition.Light,
                                WeatherCondition.Shower,
                                WeatherCondition.Rain
                            }
                        }
                    },
                    CloudLayers = new CloudLayer[]
                    {
                        new CloudLayer
                        {
                            CloudType = CloudType.Broken,
                            Altitude = 15,
                            ConvectiveCloudType = ConvectiveCloudType.Cumulonimbus
                        }
                    }
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Becoming,
                    TrendTime = new TrendTime
                    {
                        AtTime = new Time { Hours = 13, Minutes = 30 }
                    },
                    PresentWeather = new WeatherPhenomena[]
                    {
                        new WeatherPhenomena
                        {
                            WeatherConditions = new [] { WeatherCondition.NoSignificantWeather }
                        }
                    }
                },
                new Trend
                {
                    ReportType = ReportType.Trend,
                    TrendType = TrendType.Tempo,
                    MilitaryWeather = new MilitaryWeather
                    {
                        Codes = new MilitaryColorCode[]
                        {
                            MilitaryColorCode.Green,
                            MilitaryColorCode.Yellow
                        }
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(parsedTrends);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }
    }
}
