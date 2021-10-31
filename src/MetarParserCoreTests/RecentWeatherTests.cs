using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
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
        }
    }
}
