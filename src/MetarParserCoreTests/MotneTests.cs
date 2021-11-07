using System;
using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Enums;
using MetarParserCore.Objects;
using Newtonsoft.Json;
using Xunit;

namespace MetarParserCoreTests
{
    public class MotneTests
    {
        [Fact]
        public void ParseMotne_Successful()
        {
            var tokens = new[]
            {
                "R/SNOCLO",
                "R18//51109",
                "22CLRD98",
                "R24//19547",
                "R21/CLSD69",
                "R20/CLRD80",
                "36CLRD17",
                "01655967",
                "R36/116415",
                "SNOCLO",
                "20717581",
                "R12/8/4219",
                "799/0064",
                "70CLSD05",
                "R18/826152",
                "99898824",
                "R02/5/4280",
                "02796252",
                "16CLRD41",
                "R02/7/0335",
                "86412136"
            };

            var errors = new List<string>();
            var motnes = tokens.Select(token => new Motne(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(motnes.Count, 21);

            #region Valid object

            var validResultsObject = new List<Motne>
            {
                new Motne
                {
                    Specials = MotneSpecials.ClosedToSnow
                },
                new Motne
                {
                    RunwayNumber = "18",
                    TypeOfDeposit = MotneTypeOfDeposit.NotReported,
                    ExtentOfContamination = MotneExtentOfContamination.From26To50,
                    DepthOfDeposit = 11,
                    FrictionCoefficient = 9
                },
                new Motne
                {
                    RunwayNumber = "22",
                    Specials = MotneSpecials.Cleared,
                    FrictionCoefficient = 98
                },
                new Motne
                {
                    RunwayNumber = "24",
                    TypeOfDeposit = MotneTypeOfDeposit.NotReported,
                    ExtentOfContamination = MotneExtentOfContamination.Less10,
                    DepthOfDeposit = 95,
                    FrictionCoefficient = 47
                },
                new Motne
                {
                    RunwayNumber = "21",
                    Specials = MotneSpecials.Closed,
                    FrictionCoefficient = 69
                },
                new Motne
                {
                    RunwayNumber = "20",
                    Specials = MotneSpecials.Cleared,
                    FrictionCoefficient = 80
                },
                new Motne
                {
                    RunwayNumber = "36",
                    Specials = MotneSpecials.Cleared,
                    FrictionCoefficient = 17
                },
                new Motne
                {
                    RunwayNumber = "01",
                    TypeOfDeposit = MotneTypeOfDeposit.Slush,
                    ExtentOfContamination = MotneExtentOfContamination.From26To50,
                    DepthOfDeposit = 59,
                    FrictionCoefficient = 67
                },
                new Motne
                {
                    RunwayNumber = "36",
                    TypeOfDeposit = MotneTypeOfDeposit.Damp,
                    ExtentOfContamination = MotneExtentOfContamination.Less10,
                    DepthOfDeposit = 64,
                    FrictionCoefficient = 15
                },
                new Motne
                {
                    Specials = MotneSpecials.ClosedToSnow
                },
                new Motne
                {
                    RunwayNumber = "20",
                    TypeOfDeposit = MotneTypeOfDeposit.Ice,
                    ExtentOfContamination = MotneExtentOfContamination.Less10,
                    DepthOfDeposit = 75,
                    FrictionCoefficient = 81
                },
                new Motne
                {
                    RunwayNumber = "12",
                    TypeOfDeposit = MotneTypeOfDeposit.RolledSnow,
                    ExtentOfContamination = MotneExtentOfContamination.NotReported,
                    DepthOfDeposit = 42,
                    FrictionCoefficient = 19
                },
                new Motne
                {
                    RunwayNumber = "79",
                    TypeOfDeposit = MotneTypeOfDeposit.FrozenRuts,
                    ExtentOfContamination = MotneExtentOfContamination.NotReported,
                    DepthOfDeposit = 0,
                    FrictionCoefficient = 64
                },
                new Motne
                {
                    RunwayNumber = "70",
                    Specials = MotneSpecials.Closed,
                    FrictionCoefficient = 5
                },
                new Motne
                {
                    RunwayNumber = "18",
                    TypeOfDeposit = MotneTypeOfDeposit.RolledSnow,
                    ExtentOfContamination = MotneExtentOfContamination.From11To25,
                    DepthOfDeposit = 61,
                    FrictionCoefficient = 52
                },
                new Motne
                {
                    RunwayNumber = "99",
                    TypeOfDeposit = MotneTypeOfDeposit.RolledSnow,
                    ExtentOfContamination = MotneExtentOfContamination.From51To100,
                    DepthOfDeposit = 88,
                    FrictionCoefficient = 24
                },
                new Motne
                {
                    RunwayNumber = "02",
                    TypeOfDeposit = MotneTypeOfDeposit.WetSnow,
                    ExtentOfContamination = MotneExtentOfContamination.NotReported,
                    DepthOfDeposit = 42,
                    FrictionCoefficient = 80
                },
                new Motne
                {
                    RunwayNumber = "02",
                    TypeOfDeposit = MotneTypeOfDeposit.Ice,
                    ExtentOfContamination = MotneExtentOfContamination.From51To100,
                    DepthOfDeposit = 62,
                    FrictionCoefficient = 52
                },
                new Motne
                {
                    RunwayNumber = "16",
                    Specials = MotneSpecials.Cleared,
                    FrictionCoefficient = 41
                },
                new Motne
                {
                    RunwayNumber = "02",
                    TypeOfDeposit = MotneTypeOfDeposit.Ice,
                    ExtentOfContamination = MotneExtentOfContamination.NotReported,
                    DepthOfDeposit = 3,
                    FrictionCoefficient = 35
                },
                new Motne
                {
                    RunwayNumber = "86",
                    TypeOfDeposit = MotneTypeOfDeposit.DrySnow,
                    ExtentOfContamination = MotneExtentOfContamination.Less10,
                    DepthOfDeposit = 21,
                    FrictionCoefficient = 36
                }
            };

            #endregion

            var parseResults = JsonConvert.SerializeObject(motnes);
            var validResults = JsonConvert.SerializeObject(validResultsObject);
            Assert.Equal(parseResults, validResults);
        }

        [Fact]
        public void MotneParser_Unsuccessful()
        {
            var errors = new List<string>();
            var motne = new Motne(Array.Empty<string>(), errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Motne token is not found in incoming array", errors[0]);
        }

        [Fact]
        public void MotneParserEmptyToken_Unsuccessful()
        {
            var errors = new List<string>();
            var motne = new Motne(new []{ "" }, errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Motne token was not found", errors[0]);
        }

        [Fact]
        public void MotneParserWrongRunway_Unsuccessful()
        {
            var errors = new List<string>();
            var motne = new Motne(new[] { "908/4219" }, errors);

            Assert.Equal(1, errors.Count);
            Assert.Equal("Incorrect runway number in MOTNE 908/4219 token", errors[0]);
        }
    }
}
