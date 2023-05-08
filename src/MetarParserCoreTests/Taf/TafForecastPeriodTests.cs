using MetarParserCore.Objects.Supplements;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace MetarParserCoreTests.Taf
{
    public class TafForecastPeriodTests
    {
        [Fact]
        public void TafForecastPeriodParser_Successful()
        {
            const int validResultsCount = 3;

            var tokens = new[]
            {
                "1606/1615",
                "0821/0921",
                "0121/0303"
            };

            var errors = new List<string>();
            var forecastPeriods = tokens.Select(token => new TafForecastPeriod(new[] { token }, errors))
                .ToList();

            Assert.Equal(0, errors.Count);
            Assert.Equal(validResultsCount, forecastPeriods.Count);

            #region Valid object

            var validResultsObject = new List<TafForecastPeriod>
            {
                new TafForecastPeriod
                {
                    Start = new TafDayTime { Day = 16, Hours = 6 },
                    End = new TafDayTime{ Day = 16, Hours = 15 }
                },
                new TafForecastPeriod
                {
                    Start = new TafDayTime { Day = 8, Hours = 21 },
                    End = new TafDayTime{ Day = 9, Hours = 21 }
                },
                new TafForecastPeriod
                {
                    Start = new TafDayTime { Day = 1, Hours = 21 },
                    End = new TafDayTime{ Day = 3, Hours = 3 }
                },
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(forecastPeriods);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(validResults, parseResults);
        }

        [Fact]
        public void TafForecastPeriodParser_Unsuccessful()
        {
            const int validErrorsCount = 1;
            const string validErrorMessage = "TAF forecast token is not found";

            var errors = new List<string>();
            var _ = new TafForecastPeriod(Array.Empty<string>(), errors);

            Assert.Equal(validErrorsCount, errors.Count);
            Assert.Equal(validErrorMessage, errors[0]);
        }
    }
}
