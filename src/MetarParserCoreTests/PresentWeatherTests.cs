using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class PresentWeatherTests
    {
        [Fact]
        public void PresentWeather_Successful()
        {
            var tokens = new[]
            {
                "TSRAGR",
                "+BLSN",
                "VCSHRA",
                "-RADZ",
                "NSW",
                "VCTS"
            };

            var errors = new List<string>();
            var presentWeathers = tokens.Select(token => new WeatherPhenomena(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(presentWeathers.Count, 6);

            #region Valid object

            var validResultsObject = new List<WeatherPhenomena>
            {
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Thunderstorm,
                        WeatherCondition.Rain,
                        WeatherCondition.Hail
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Heavy,
                        WeatherCondition.Blowing,
                        WeatherCondition.Snow
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Vicinity,
                        WeatherCondition.Shower,
                        WeatherCondition.Rain
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Light,
                        WeatherCondition.Rain,
                        WeatherCondition.Drizzle
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.NoSignificantWeather
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Vicinity,
                        WeatherCondition.Thunderstorm
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(presentWeathers);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void PresentWeatherParser_Unsuccessful()
        {
            var errors = new List<string>();
            var weather = new WeatherPhenomena(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array of present weather tokens is empty", errors[0]);
        }

        [Fact]
        public void UnexpectedToken_Unsuccessful()
        {
            var errors = new List<string>();
            var weather = new WeatherPhenomena(new []{ "+" }, errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal("Cannot parse weather token: \"+\"", errors[0]);
        }
    }
}
