using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.Objects.Supplements;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class SurfaceWindTests
    {
        [Fact]
        public void SurfaceWindParser_Successful()
        {
            const int validResultsCount = 11;

            var unparsedWinds = new[]
            {
                new string[] {"27005MPS"},
                new string[] {"VRB01MPS"},
                new string[] {"35004MPS", "300V050"},
                new string[] {"19008G13MPS"},
                new string[] {"19008G13MPS", "300V050"},
                new string[] {"27005KT"},
                new string[] {"30015G25KT"},
                new string[] {"VRB02KT"},
                new string[] {"27005KT", "060V130"},
                new string[] {"240P49KT"},
                new string[] {"030P99MPS"}
            };

            var errors = new List<string>();
            var surfaceWinds = unparsedWinds.Select(x => new SurfaceWind(x, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, surfaceWinds.Count);

            #region Valid object

            var validResultsObject = new List<SurfaceWind>
            {
                new SurfaceWind 
                {
                    Direction = 270,
                    Speed = 5,
                    WindUnit = WindUnit.MetersPerSecond
                },
                new SurfaceWind
                {
                    IsVariable = true,
                    Speed = 1,
                    WindUnit = WindUnit.MetersPerSecond
                },
                new SurfaceWind
                {
                    Direction = 350,
                    Speed = 4,
                    WindUnit = WindUnit.MetersPerSecond,
                    ExtremeWindDirections = new ExtremeWindDirections
                    {
                        FirstExtremeDirection = 300,
                        LastExtremeWindDirection = 50
                    }
                },
                new SurfaceWind
                {
                    Direction = 190,
                    Speed = 8,
                    WindUnit = WindUnit.MetersPerSecond,
                    GustSpeed = 13
                },
                new SurfaceWind
                {
                    Direction = 190,
                    Speed = 8,
                    WindUnit = WindUnit.MetersPerSecond,
                    GustSpeed = 13,
                    ExtremeWindDirections = new ExtremeWindDirections
                    {
                        FirstExtremeDirection = 300,
                        LastExtremeWindDirection = 50
                    }
                },
                new SurfaceWind
                {
                    Direction = 270,
                    Speed = 5,
                    WindUnit = WindUnit.Knots
                },
                new SurfaceWind
                {
                    Direction = 300,
                    Speed = 15,
                    WindUnit = WindUnit.Knots,
                    GustSpeed = 25
                },
                new SurfaceWind
                {
                    IsVariable = true,
                    Speed = 2,
                    WindUnit = WindUnit.Knots
                },
                new SurfaceWind
                {
                    Direction = 270,
                    Speed = 5,
                    WindUnit = WindUnit.Knots,
                    ExtremeWindDirections = new ExtremeWindDirections
                    {
                        FirstExtremeDirection = 60,
                        LastExtremeWindDirection = 130
                    }
                },
                new SurfaceWind
                {
                    GreaterThanSign = true,
                    Direction = 240,
                    Speed = 49,
                    WindUnit = WindUnit.Knots
                },
                new SurfaceWind
                {
                    GreaterThanSign = true,
                    Direction = 30,
                    Speed = 99,
                    WindUnit = WindUnit.MetersPerSecond
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(surfaceWinds);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void SurfaceWindParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Wind tokens were not found";

            var errors = new List<string>();
            var _ = new SurfaceWind(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }
    }
}
