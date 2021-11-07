using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class SeaConditionTests
    {
        [Fact]
        public void SeaConditionParser_Successful()
        {
            var tokens = new[]
            {
                "W24/S8",
                "WM5/S0",
                "W31/H32",
                "W24/S4",
                "WM2/S2",
                "W11/S4",
                "W15/H172",
            };

            var errors = new List<string>();
            var seaConditions = tokens.Select(token => new SeaCondition(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(seaConditions.Count, 7);

            #region Valid object

            var validResultsObject = new List<SeaCondition>
            {
                new SeaCondition
                {
                    SeaTemperature = 24,
                    SeaState = SeaStateType.VeryHigh
                },
                new SeaCondition
                {
                    SeaTemperature = -5,
                    SeaState = SeaStateType.Glassy
                },
                new SeaCondition
                {
                    SeaTemperature = 31,
                    WaveHeight = 32,
                    SeaState = SeaStateType.None
                },
                new SeaCondition
                {
                    SeaTemperature = 24,
                    SeaState = SeaStateType.Moderate
                },
                new SeaCondition
                {
                    SeaTemperature = -2,
                    SeaState = SeaStateType.Wavelets
                },
                new SeaCondition
                {
                    SeaTemperature = 11,
                    SeaState = SeaStateType.Moderate
                },
                new SeaCondition
                {
                    SeaTemperature = 15,
                    WaveHeight = 172,
                    SeaState = SeaStateType.None
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(seaConditions);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void SeaConditionParser_Unsuccessful()
        {
            var errors = new List<string>();
            var cloudLayer = new SeaCondition(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array of sea condition tokens is empty", errors[0]);
        }
    }
}
