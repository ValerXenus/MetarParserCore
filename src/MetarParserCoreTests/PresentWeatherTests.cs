using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
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
                "VCSH",
                "-RADZ"
            };

            var errors = new List<string>();
            var presentWeathers = tokens.Select(token => new WeatherPhenomena(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(presentWeathers.Count, 4);
        }
    }
}
