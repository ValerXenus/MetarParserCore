using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class TemperatureInfoTests
    {
        [Fact]
        public void TemperatureInfo_Successful()
        {
            var tokens = new[]
            {
                "M05/M08",
                "M01/04",
                "4/M12",
                "05/08"
            };

            var errors = new List<string>();
            var temperatureInfos = tokens.Select(token => new TemperatureInfo(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(temperatureInfos.Count, 4);

            #region Valid object

            var validResultsObject = new List<TemperatureInfo>
            {
                new TemperatureInfo
                {
                    Value = -5,
                    DewPoint = -8
                },
                new TemperatureInfo
                {
                    Value = -1,
                    DewPoint = 4
                },
                new TemperatureInfo
                {
                    Value = 4,
                    DewPoint = -12
                },
                new TemperatureInfo
                {
                    Value = 5,
                    DewPoint = 8
                },
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(temperatureInfos);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void TemperatureInfoEmptyTokensArray_Unsuccessful()
        {
            var errors = new List<string>();
            var temperatureInfo = new TemperatureInfo(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array with temperature token is empty", errors[0]);
        }

        [Fact]
        public void TemperatureInfoEmptyToken_Unsuccessful()
        {
            var errors = new List<string>();
            var temperatureInfo = new TemperatureInfo(new []{ "" }, errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Cannot parse empty temperature token", errors[0]);
        }

        [Fact]
        public void TemperatureInfoWrongToken_Unsuccessful()
        {
            var errors = new List<string>();
            var temperatureInfo = new TemperatureInfo(new[] { "21/" }, errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Cannot parse \"21/\" as temperature token", errors[0]);
        }
    }
}
