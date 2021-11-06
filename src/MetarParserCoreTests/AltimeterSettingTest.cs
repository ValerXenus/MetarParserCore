using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class AltimeterSettingTests
    {
        [Fact]
        public void AltimeterSetting_Successful()
        {
            var tokens = new[]
            {
                "A3012",
                "Q1019"
            };

            var errors = new List<string>();
            var altimeterSetting = tokens.Select(token => new AltimeterSetting(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(altimeterSetting.Count, 2);
        }
    }
}
