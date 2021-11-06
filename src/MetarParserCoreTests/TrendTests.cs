using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using MetarParserCore.TokenLogic;
using Xunit;

namespace MetarParserCoreTests
{
    public class TrendTests
    {
        [Fact]
        public void CloudLayer_Successful()
        {
            var trends = new[]
            {
                "BECMG AT1330 02020MPS TEMPO VRB15MPS -TSRA BKN020CB OVC110",
                "BECMG AT1330 02020MPS TEMPO VRB15MPS -TSRA BKN020CB",
                "NOSIG",
                "BECMG AT1330 02020MPS",
                "TEMPO VRB15MPS -TSRA BKN020CB OVC110",
                "TEMPO 2100 -SHRA BKN015CB",
                "BECMG AT1330 NSW"
            };

            var errors = new List<string>();
            var outcome = new List<Trend>();

            foreach (var trend in trends)
            {
                var splitted = trend.ToUpper().Split(" ");
                var reports = Recognizer.Instance().RecognizeAndGroupTokensTrend(splitted);

                foreach (var report in reports)
                {
                    var current = new Trend(report, Month.None);
                    if (current.ParseErrors is { Length: > 0 })
                    {
                        errors = errors.Concat(current.ParseErrors).ToList();
                        continue;
                    }

                    outcome.Add(current);
                }
            }

            Assert.Equal(errors.Count, 0);
            Assert.Equal(outcome.Count, 9);
        }
    }
}
