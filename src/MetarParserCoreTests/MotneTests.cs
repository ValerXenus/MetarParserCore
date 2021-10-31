using System.Collections.Generic;
using System.Linq;
using MetarParserCore.Objects;
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
                "R20/CLRD00",
                "R04/CLRD24",
                "R29/CLRD55",
                "36CLRD17",
                "01655967",
                "R36/116415",
                "R26/590382",
                "SNOCLO",
                "20717581",
                "R12/8/4219",
                "799/0064",
                "70CLSD05",
                "88CLSD09",
                "R18/826152",
                "19CLSD31",
                "99898824",
                "R02/5/4280",
                "02796252",
                "R31/CLRD78",
                "R07/CLRD65",
                "29CLSD25",
                "R18/CLSD19",
                "R08/CLRD61",
                "16CLRD41",
                "R02/7/0335",
                "86412136",
                "11CLSD87",
                "R32/CLSD85"
            };

            var errors = new List<string>();
            var motnes = tokens.Select(token => new Motne(new[] { token }, errors))
                .ToList();

            Assert.Equal(errors.Count, 0);
            Assert.Equal(motnes.Count, 33);

        }
    }
}
