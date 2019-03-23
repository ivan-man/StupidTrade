using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AlphaVantageConnector.Helpers
{
    public static class DeserializationHelper
    {
        public static string ClearResponse(string response)
        {
            //remove numbers of fields
            return Regex.Replace(response, "([0-9]+. )", string.Empty); //+||[ \t]+
        }

        //public 
    }
}
