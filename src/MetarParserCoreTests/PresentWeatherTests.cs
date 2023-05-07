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
            const int validResultsCount = 5;

            var tokens = new[]
            {
                "TSRAGR",
                "+BLSN",
                "VCSHRA",
                "-RADZ",
                "NSW"
            };

            var errors = new List<string>();
            var presentWeathers = tokens.Select(token => new WeatherPhenomena(new[] { token }, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, presentWeathers.Count);

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
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(presentWeathers);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void PresentWeatherParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorMessage = "Array of present weather tokens is empty";

            var errors = new List<string>();
            var _ = new WeatherPhenomena(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorMessage, errors[0]);
        }

        [Fact]
        public void UnexpectedToken_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorMessage = "Cannot parse weather token: \"+\"";

            var errors = new List<string>();
            var _ = new WeatherPhenomena(new []{ "+" }, errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorMessage, errors[0]);
        }
    }
}
