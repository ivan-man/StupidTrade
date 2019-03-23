using AlphaVantageConnector.Resources;
using System.Text.RegularExpressions;

namespace AlphaVantageConnector.Helpers
{
    public static class PreDeserializationHelper
    {
        public static string ClearResponse(string response)
        {
            //remove numbers of fields
            var result = Regex.Replace(response, @"[0-9]{1,2}\. ", string.Empty); //+||[ \t]+

            if (response.Contains(AvResources.TimeSeries))
            {
                Regex.Replace(result, $@"{AvResources.TimeSeries} \(({string.Join("|", Intervals.Values)})\)", "TimeSeries");
            }

            return result; //+||[ \t]+
        }

        //public 
    }
}
