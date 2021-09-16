using MetarParserCore;
using Xunit;

namespace MetarParserCoreTests
{
    public class EntireMetarTests
    {
        [Fact]
        public void ParseMetarExample1_Successful()
        {
            var rawString = "";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse("UWKD 291400Z 33004MPS 300V360 CAVOK 20/00 Q1019 R29/CLRD70 NOSIG RMK QFE753/1004=");

        }
    }
}
