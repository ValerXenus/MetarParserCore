namespace MetarParserCore.Parsers
{
    /// <summary>
    /// Regular expressions for detect token type
    /// </summary>
    public class TokenRegularExpressions
    {
        public static string Airport => @"^[A-Z]{4}$";

        public static string ObservationDayTime => @"^[0-3]{1}[0-9]{1}[0-2]{1}[0-9]{1}[0-5]{1}[0-9]{1}Z$";

        public static string Modifier => @"^(COR|AUTO)$";

        public static string SurfaceWind => @"^([0-3]{1}[0-9]{2}[0-9]{2}(G[0-9]{2}){0,1}(MPS|KMT|KT){1}|[0-3]{1}[0-9]{2}V[0-3]{1}[0-9]{2})$";


    }
}
