using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
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
                "BKN100CB",
                "OVC///",
                "NSC"
            };

            var errors = new List<string>();
            var cloudLayers = tokens.Select(token => new CloudLayer(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(cloudLayers.Count, 7);

            #region Valid object

            var validResultsObject = new List<CloudLayer>
            {
                new CloudLayer
                {
                    CloudType = CloudType.Broken,
                    Altitude = 8
                },
                new CloudLayer
                {
                    CloudType = CloudType.Overcast,
                    Altitude = 40
                },
                new CloudLayer
                {
                    CloudType = CloudType.VerticalVisibility,
                    Altitude = 230
                },
                new CloudLayer
                {
                    CloudType = CloudType.Scattered,
                    Altitude = 25,
                    ConvectiveCloudType = ConvectiveCloudType.ToweringCumulus
                },
                new CloudLayer
                {
                    CloudType = CloudType.Broken,
                    Altitude = 100,
                    ConvectiveCloudType = ConvectiveCloudType.Cumulonimbus
                },
                new CloudLayer
                {
                    CloudType = CloudType.Overcast,
                    IsCloudBelow = true
                },
                new CloudLayer
                {
                    CloudType = CloudType.NoSignificantClouds
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(cloudLayers);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void CloudLayerParser_Unsuccessful()
        {
            var errors = new List<string>();
            var cloudLayer = new CloudLayer(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array of cloud layer tokens is empty", errors[0]);
        }


        [Fact]
        public void UnexpectedToken_Unsuccessful()
        {
            var errors = new List<string>();
            var cloudLayer = new CloudLayer(new[] { "ERROR1" }, errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal("Cannot parse altitude from token OR1", errors[0]);
        }
    }
}
