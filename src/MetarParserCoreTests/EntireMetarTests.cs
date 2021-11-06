using MetarParserCore;
using Xunit;

namespace MetarParserCoreTests
{
    public class EntireMetarTests
    {
        [Fact]
        public void ParseMetarExample1_Successful()
        {
            var rawString = "UWKD 021330Z 17007G12MPS 5000 1500SE R29/1500D SHRA BKN015CB 17/14 Q1001"
                + " WS TKOF RWY20 R29/2/0050 BECMG AT1330 02020MPS TEMPO VRB15MPS -TSRA BKN020CB OVC110 RMK QFE740/0987=";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Airport, "UWKD");
        }

        [Fact]
        public void ParseMetarExample2_Successful()
        {
            var rawString = "UWKD 020500Z NIL=";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Airport, "UWKD");
            Assert.Equal(airportMetar.IsNil, true);
        }

        [Fact]
        public void ParseMetarExample3_Successful()
        {
            var rawString = "KSME 171053Z AUTO 00000KT 1 3/4SM BR VV007";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Airport, "KSME");
        }

        [Fact]
        public void ParseMetarExample4_Successful()
        {
            var rawString = "KSME 171053Z AUTO 00000KT 1 3/4SM ERROR BR VV007";
            var metarParser = new MetarParser();
            var airportMetar = metarParser.Parse(rawString);

            Assert.Null(airportMetar.ParseErrors);
            Assert.Equal(airportMetar.Airport, "KSME");
            Assert.Equal(airportMetar.Unrecognized[0], "ERROR");
        }
    }
}
