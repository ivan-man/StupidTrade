using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Resources;
using System;
using System.Text.RegularExpressions;

namespace AlphaVantageConnector.Helpers
{
    public static class PreDeserializationHelper
    {
        public static string ClearResponse(string response)
        {
            //remove numbers of fields
            var result = Regex.Replace(response, @"\""[0-9]{1,2}[\.:] ", @"""");

            if (response.Contains(AvResources.TimeSeries))
            {
                result = Regex.Replace(result, $@"({AvResources.TimeSeries} \([0-9a-zA-Z]+\))|([a-zA-Z]+ {AvResources.TimeSeries})", "TimeSeries");
            }

            if (response.Contains(AvResources.GlobalQuote))
            {
                result = Regex.Replace(result, $@"([ %])", string.Empty);
            }

            if (response.Contains(AvResources.TechnicalAnalysis))
            {
                var indicators = Enum.GetValues(typeof(TechnicalIndicators)) as TechnicalIndicators[];
                result = Regex.Replace(result, $@"({AvResources.TechnicalAnalysis}: ({string.Join("|", indicators)}))", "TechnicalAnalysis");
            }

            return result;
        }
    }
}
