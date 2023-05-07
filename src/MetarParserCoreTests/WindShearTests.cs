using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class WindShearTests
    {
        [Fact]
        public void ParseWindShear_Successful()
        {
            const int validResultsCount = 6;

            var tokensArray = new []
            {
                new [] { "WS", "ALL", "RWY" },
                new [] { "WS", "TKOF", "R06" },
                new [] { "WS", "LDG", "R29R" },
                new [] { "WS", "R06LL" },
                new [] { "WS", "TKOF", "RWY29R" },
                new [] { "WS", "RWY06LL" }
            };

            var errors = new List<string>();
            var windShears = tokensArray.Select(wsTokens => new WindShear(wsTokens, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, windShears.Count);

            #region Valid object

            var validResultsObject = new List<WindShear>
            {
                new WindShear
                {
                    IsAll = true
                },
                new WindShear
                {
                    Type = WindShearType.TakeOff,
                    Runway = "06"
                },
                new WindShear
                {
                    Type = WindShearType.Landing,
                    Runway = "29R"
                },
                new WindShear
                {
                    Runway = "06LL"
                },
                new WindShear
                {
                    Type = WindShearType.TakeOff,
                    Runway = "29R"
                },
                new WindShear
                {
                    Runway = "06LL"
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(windShears);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void WindShearParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Array with wind shear tokens is incorrect";

            var errors = new List<string>();
            var windShear = new WindShear(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }
    }
}
