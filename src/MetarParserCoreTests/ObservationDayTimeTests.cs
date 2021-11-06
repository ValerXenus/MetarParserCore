using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
using MetarParserCore.Objects.Supplements;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class ObservationDayTimeTests
    {
        [Fact]
        public void ObservationDayTimeParser_Successful()
        {
            var tokens = new[]
            {
                "291030Z",
                "140300Z",
                "250700Z",
                "021000Z"
            };

            var errors = new List<string>();
            var dayTimes = tokens.Select(token => new ObservationDayTime(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(dayTimes.Count, 4);

            #region Valid object

            var validResultsObject = new List<ObservationDayTime>
            {
                new ObservationDayTime
                {
                    Day = 29,
                    Time = new Time
                    {
                        Hours = 10,
                        Minutes = 30
                    }
                },
                new ObservationDayTime
                {
                    Day = 14,
                    Time = new Time
                    {
                        Hours = 3,
                        Minutes = 0
                    }
                },
                new ObservationDayTime
                {
                    Day = 25,
                    Time = new Time
                    {
                        Hours = 7,
                        Minutes = 0
                    }
                },
                new ObservationDayTime
                {
                    Day = 2,
                    Time = new Time
                    {
                        Hours = 10,
                        Minutes = 0
                    }
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(dayTimes);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void ObservationDayTimeParser_Unsuccessful()
        {
            var errors = new List<string>();
            var dayTime = new ObservationDayTime(Array.Empty<string>(), errors);

            Assert.Equal(errors.Count, 1);
            Assert.Equal(errors[0], "Array of observation day time tokens is empty");
        }
    }
}
