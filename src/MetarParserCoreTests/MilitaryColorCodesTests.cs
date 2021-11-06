using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class MilitaryColorCodesTests
    {
        [Fact]
        public void MilitaryColorCodesParse_Successful()
        {
            var codesCollection = new []
            {
                new [] { "GRN", "AMB" },
                new [] { "RED", "AMB" },
                new [] { "BLACKGRN"}
            };

            var errors = new List<string>();
            var outcome = codesCollection
                .Select(codes => new MilitaryWeather(codes, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(outcome.Count, 3);
        }
    }
}
