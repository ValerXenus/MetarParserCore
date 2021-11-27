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
            var visibilities = new[]
            {
                new string[] {"1", "3/4SM"},
                new string[] {"4/5SM"},
                new string[] {"9999"},
                new string[] {"5000", "1500SE"},
                new string[] {"1200NW"},
                new string[] {"CAVOK"}

            };

            var errors = new List<string>();
            var prevailingVisibilities = visibilities.Select(x => new PrevailingVisibility(x, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(prevailingVisibilities.Count, 6);

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
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(prevailingVisibilities);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void PrevailingVisibilityParser_Unsuccessful()
        {
            var errors = new List<string>();
            var prevailingVisibility = new PrevailingVisibility(Array.Empty<string>(), errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal(errors[0], "Array of prevailing visibility tokens is empty");
        }

        [Fact]
        public void UnexpectedToken_Unsuccessful()
        {
            var errors = new List<string>();
            var prevailingVisibility = new PrevailingVisibility(new []{ "ERROR" }, errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal(errors[0], "Unexpected token: ERROR");
        }
    }
}
