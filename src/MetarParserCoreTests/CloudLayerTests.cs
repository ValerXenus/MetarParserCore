using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using Xunit;

namespace MetarParserCoreTests
{
    public class CloudLayerTests
    {
        [Fact]
        public void CloudLayer_Successful()
        {
            var tokens = new[]
            {
                "BKN008",
                "OVC040",
                "VV230",
                "SCT025TCU",
                "BKN100CU",
                "OVC///",
                "NSC"
            };

            var errors = new List<string>();
            var cloudLayers = tokens.Select(token => new CloudLayer(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(cloudLayers.Count, 7);
        }
    }
}
