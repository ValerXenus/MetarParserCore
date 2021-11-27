using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class RunwayVisualRangeTests
    {
        [Fact]
        public void ParseRvr_Successful()
        {
            var tokens = new []
            {
                "R26/M8000FT",
                "R30RR/3000V5500N",
                "R20C/M1000",
                "R12/0500V1000FT/U",
                "R07C/P7000FT/D",
                "R23LL/7000V8000FT/N"
            };

            var errors = new List<string>();
            var rvrs = tokens.Select(token => new RunwayVisualRange(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(rvrs.Count, 6);

            #region Valid object

            var validResultsObject = new List<RunwayVisualRange>
            {
                new RunwayVisualRange
                {
                    RunwayNumber = "26",
                    MeasurableBound = MeasurableBound.Lower,
                    UnitType = RvrUnitType.Feets,
                    VisibilityValue = 8000
                },
                new RunwayVisualRange
                {
                    RunwayNumber = "30RR",
                    UnitType = RvrUnitType.Meters,
                    VisibilityValue = 3000,
                    VisibilityValueMax = 5500,
                    RvrTrend = RvrTrend.NoChange
                },
                new RunwayVisualRange
                {
                    RunwayNumber = "20C",
                    MeasurableBound = MeasurableBound.Lower,
                    UnitType = RvrUnitType.Meters,
                    VisibilityValue = 1000
                },
                new RunwayVisualRange
                {
                    RunwayNumber = "12",
                    VisibilityValue = 500,
                    VisibilityValueMax = 1000,
                    UnitType = RvrUnitType.Feets,
                    RvrTrend = RvrTrend.Upward
                },
                new RunwayVisualRange
                {
                    RunwayNumber = "07C",
                    MeasurableBound = MeasurableBound.Higher,
                    VisibilityValue = 7000,
                    RvrTrend = RvrTrend.Downward,
                    UnitType = RvrUnitType.Feets
                },
                new RunwayVisualRange
                {
                    RunwayNumber = "23LL",
                    VisibilityValue = 7000,
                    VisibilityValueMax = 8000,
                    UnitType = RvrUnitType.Feets,
                    RvrTrend = RvrTrend.NoChange
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(rvrs);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void PrevailingVisibilityParser_Unsuccessful()
        {
            var errors = new List<string>();
            var rvr = new RunwayVisualRange(Array.Empty<string>(), errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal(errors[0], "Array of runway visual range tokens is empty");
        }
    }
}
