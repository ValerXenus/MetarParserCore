using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class MilitaryColorCodesTests
    {
        [Fact]
        public void MilitaryColorCodesParse_Successful()
        {
            var codesCollection = new []
            {
                new [] { "GRN", "AMB" },
                new [] { "RED", "AMB" },
                new [] { "BLACKGRN"}
            };

            var errors = new List<string>();
            var outcome = codesCollection
                .Select(codes => new MilitaryWeather(codes, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(outcome.Count, 3);

            #region Valid object

            var validResultsObject = new List<MilitaryWeather>
            {
                new MilitaryWeather
                {
                    Codes = new []
                    {
                        MilitaryColorCode.Green,
                        MilitaryColorCode.Amber
                    }
                },
                new MilitaryWeather
                {
                    Codes = new []
                    {
                        MilitaryColorCode.Red,
                        MilitaryColorCode.Amber
                    }
                },
                new MilitaryWeather
                {
                    IsClosed = true,
                    Codes = new []
                    {
                        MilitaryColorCode.Green
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(outcome);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void CloudLayerParser_Unsuccessful()
        {
            var errors = new List<string>();
            var militaryWeather = new MilitaryWeather(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Array of military codes is empty", errors[0]);
        }
    }
}
