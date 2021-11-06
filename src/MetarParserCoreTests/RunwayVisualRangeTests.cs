using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class RunwayVisualRangeTests
    {
        [Fact]
        public void ParseRvr_Successful()
        {
            var tokens = new []
            {
                "R26/M8000FT",
                "R30RR/3000V5500N",
                "R20C/M1000",
                "R12/0500V1000FT/U",
                "R07C/P7000FT/N",
                "R23LL/7000V8000FT/N"
            };

            var errors = new List<string>();
            var rvrs = tokens.Select(token => new RunwayVisualRange(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(rvrs.Count, 6);
            
        }
    }
}
