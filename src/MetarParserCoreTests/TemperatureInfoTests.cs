using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class TemperatureInfoTests
    {
        [Fact]
        public void CloudLayer_Successful()
        {
            var tokens = new[]
            {
                "M05/M08",
                "M01/04",
                "24/M12",
                "05/08"
            };

            var errors = new List<string>();
            var temperatureInfos = tokens.Select(token => new TemperatureInfo(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(temperatureInfos.Count, 4);
        }
    }
}
