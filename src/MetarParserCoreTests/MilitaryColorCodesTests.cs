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
            const int validOutcomeCount = 3;

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

            Assert.Equal(0, errors.Count);
            Assert.Equal(validOutcomeCount, outcome.Count);

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
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void CloudLayerParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorMessage = "Array of military codes is empty";

            var errors = new List<string>();
            var _ = new MilitaryWeather(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorMessage, errors[0]);
        }
    }
}
