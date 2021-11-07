using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
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
            var altimeterSettings = tokens.Select(token => new AltimeterSetting(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(altimeterSettings.Count, 2);

            #region Valid object

            var validResultsObject = new List<AltimeterSetting>
            {
                new AltimeterSetting
                {
                    Value = 3012,
                    UnitType = AltimeterUnitType.InchesOfMercury
                },
                new AltimeterSetting
                {
                    Value = 1019,
                    UnitType = AltimeterUnitType.Hectopascal
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(altimeterSettings);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void CloudLayerParser_Unsuccessful()
        {
            var errors = new List<string>();
            var cloudLayer = new AltimeterSetting(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array with altimeter token is empty", errors[0]);
        }
    }
}
