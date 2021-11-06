using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class SeaConditionTests
    {
        [Fact]
        public void SeaConditionParser_Successful()
        {
            var tokens = new[]
            {
                "W24/S8",
                "WM11/S0",
                "W31/H32",
                "W24/S4",
                "WM11/S2",
                "W11/S4",
                "W15/H172",
            };

            var errors = new List<string>();
            var seaConditions = tokens.Select(token => new SeaCondition(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(seaConditions.Count, 7);
        }
    }
}
