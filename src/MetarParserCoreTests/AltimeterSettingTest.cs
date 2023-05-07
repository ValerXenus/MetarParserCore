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
            const int validParsedSettingsCount = 2;

            var tokens = new[]
            {
                "A3012",
                "Q1019"
            };

            var errors = new List<string>();
            var altimeterSettings = tokens.Select(token => new AltimeterSetting(new[] { token }, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validParsedSettingsCount, altimeterSettings.Count);

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
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void CloudLayerParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorMessage = "Array with altimeter token is empty";

            var errors = new List<string>();
            var _ = new AltimeterSetting(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorMessage, errors[0]);
        }
    }
}
