using System;
using System.Collections.Generic;
using System.Linq;
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
            const int validResultsCount = 4;

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

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, temperatureInfos.Count);

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
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void TemperatureInfoWithoutDewPoint_Successful()
        {
            var errors = new List<string>();
            var temperatureInfo = new TemperatureInfo(new[] { "21/" }, errors);

            #region Valid object

            var validResultObject = new TemperatureInfo
            {
                Value = 21,
                DewPoint = int.MinValue
            };

            #endregion

            var parseResult = JsonConvert.SerializeObject(temperatureInfo);
            var validResult = JsonConvert.SerializeObject(validResultObject);

            Assert.Equal(validResult, parseResult);
        }

        [Fact]
        public void TemperatureInfoEmptyTokensArray_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Array with temperature token is empty";

            var errors = new List<string>();
            var _ = new TemperatureInfo(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }

        [Fact]
        public void TemperatureInfoEmptyToken_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Cannot parse empty temperature token";

            var errors = new List<string>();
            var _ = new TemperatureInfo(new []{ "" }, errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }
    }
}
