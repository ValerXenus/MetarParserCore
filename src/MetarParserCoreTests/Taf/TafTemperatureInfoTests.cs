using MetarParserCore.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects.Supplements;
using Xunit;

namespace MetarParserCoreTests.Taf
{
    public class TafTemperatureInfoTests
    {
        [Fact]
        public void TemperatureInfo_Successful()
        {
            const int validResultsCount = 3;

            var tokens = new[]
            {
                "TXM03/1012Z",
                "TNM12/1106Z",
                "TN24/0310Z"
            };

            var errors = new List<string>();
            var temperatureInfos = tokens.Select(token => new TafTemperatureInfo(new[] { token }, errors)).ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, temperatureInfos.Count);

            #region Valid object

            var validResultsObject = new List<TafTemperatureInfo>
            {
                new TafTemperatureInfo
                {
                    MaxForecastValue = new TafTemperatureValue
                    {
                        Value = -3,
                        Day = 10,
                        Hours = 12
                    }
                },
                new TafTemperatureInfo
                {
                    MinForecastValue = new TafTemperatureValue
                    {
                        Value = -12,
                        Day = 11,
                        Hours = 6
                    }
                },
                new TafTemperatureInfo
                {
                    MinForecastValue = new TafTemperatureValue
                    {
                        Value = 24,
                        Day = 3,
                        Hours = 10
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(temperatureInfos);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void TemperatureInfoEmptyTokensArray_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Array with forecast temperature tokens is empty";

            var errors = new List<string>();
            var _ = new TafTemperatureInfo(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }

        [Fact]
        public void TemperatureInfoEmptyToken_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Cannot parse \"-10/\" as forecast temperature token";

            var errors = new List<string>();
            var _ = new TafTemperatureInfo(new[] { "M10/" }, errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }
    }
}
