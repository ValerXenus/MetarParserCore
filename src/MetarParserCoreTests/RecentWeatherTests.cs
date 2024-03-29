﻿using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class RecentWeatherTests
    {
        [Fact]
        public void RecentWeather_Successful()
        {
            var tokens = new[]
            {
                "REFZRA",
                "RERA",
                "REBLSN",
                "RETSGR"
            };

            var errors = new List<string>();
            var recentWeathers = tokens.Select(token => new WeatherPhenomena(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(recentWeathers.Count, 4);

            #region Valid object

            var validResultsObject = new List<WeatherPhenomena>
            {
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Freezing,
                        WeatherCondition.Rain
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Rain
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Blowing,
                        WeatherCondition.Snow
                    }
                },
                new WeatherPhenomena
                {
                    WeatherConditions = new []
                    {
                        WeatherCondition.Thunderstorm,
                        WeatherCondition.Hail
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(recentWeathers);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }
    }
}
