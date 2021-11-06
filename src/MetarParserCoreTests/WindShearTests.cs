using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class WindShearTests
    {
        [Fact]
        public void ParseWindShear_Successful()
        {
            var tokensArray = new []
            {
                new [] { "WS", "ALL", "RWY" },
                new [] { "WS", "TKOF", "06" },
                new [] { "WS", "LDG", "R29R" },
                new [] { "WS", "R06LL" },
                new [] { "WS", "TKOF", "RWY29R" },
                new [] { "WS", "RWY06LL" }
            };

            var errors = new List<string>();
            var windShears = tokensArray.Select(wsTokens => new WindShear(wsTokens, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(windShears.Count, 6);

        }
    }
}
