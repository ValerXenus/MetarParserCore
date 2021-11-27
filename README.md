# MetarParserCore
A .NET Core library intended for parsing raw METAR data.

# Getting started
This library is easy in use. Just follow exanple below:

```cs
// Input raw METAR string
var raw = "UWKD 291500Z 32003MPS CAVOK 18/02 Q1019 R29/CLRD70 NOSIG RMK QFE753/1004=";
// Initialize METAR parser
var metarParser = new MetarParser();
// Parse raw METAR
var airportMetar = metarParser.Parse(raw);
```

# Classes overview



# Feedback

If you have any feedback, contact me valeraxenus@mail.ru