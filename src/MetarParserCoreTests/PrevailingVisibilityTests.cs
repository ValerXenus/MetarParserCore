using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.Objects.Supplements;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class PrevailingVisibilityTests
    {
        [Fact]
        public void PrevailingVisibilityParser_Successful()
        {
            const int validResultsCount = 8;

            var visibilities = new[]
            {
                new string[] {"1", "3/4SM"},
                new string[] {"4/5SM"},
                new string[] {"9999"},
                new string[] {"5000", "1500SE"},
                new string[] {"1200NW"},
                new string[] {"CAVOK"},
                new string[] {"P6SM"},
                new string[] {"M1/2SM"}
            };

            var errors = new List<string>();
            var prevailingVisibilities = visibilities.Select(x => new PrevailingVisibility(x, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, prevailingVisibilities.Count);

            #region Valid object

            var validResultsObject = new List<PrevailingVisibility>
            {
                new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        WholeNumber = 1,
                        Numerator = 3,
                        Denominator = 4
                    }
                },
                new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        Numerator = 4,
                        Denominator = 5
                    }
                },
                new PrevailingVisibility
                {
                    VisibilityInMeters = new VisibilityInMeters
                    {
                        VisibilityValue = 9999
                    }
                },
                new PrevailingVisibility
                {
                    VisibilityInMeters = new VisibilityInMeters
                    {
                        VisibilityValue = 5000,
                        MaxVisibilityValue = 1500,
                        MaxVisibilityDirection = VisibilityDirection.SouthEast
                    }
                },
                new PrevailingVisibility
                {
                    VisibilityInMeters = new VisibilityInMeters
                    {
                        VisibilityValue = 1200,
                        VisibilityDirection = VisibilityDirection.NorthWest
                    }
                },
                new PrevailingVisibility
                {
                    IsCavok = true
                },
                new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        GreaterThanSign = true,
                        WholeNumber = 6
                    }
                },
                new PrevailingVisibility
                {
                    VisibilityInStatuteMiles = new VisibilityInStatuteMiles
                    {
                        LessThanSign = true,
                        Numerator = 1,
                        Denominator = 2
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(prevailingVisibilities);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void PrevailingVisibilityParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Array of prevailing visibility tokens is empty";

            var errors = new List<string>();
            var _ = new PrevailingVisibility(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }

        [Fact]
        public void UnexpectedToken_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorsMessage = "Unexpected token: ERROR";

            var errors = new List<string>();
            var _ = new PrevailingVisibility(new []{ "ERROR" }, errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorsMessage, errors[0]);
        }
    }
}
