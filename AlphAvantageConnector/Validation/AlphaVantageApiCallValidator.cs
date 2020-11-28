using AlphaVantageConnector.Dictionaries;
using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Helpers;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlphaVantageConnector.Validation
{
    /// <summary>
    /// Not strong validation of AV API url, but API function must be first.
    /// </summary>
    public static class AlphaVantageApiCallValidator 
    {
        private static readonly string _callPattern;
        private static Regex _urlRegex;

        static AlphaVantageApiCallValidator()
        {
            //Examples:
            //from_symbol=EUR
            //to_symbol=EUR
            //datatype = csv
            //market=CNY
            //series_type: close, open, high, low
            //slowlimit=0.01
            //slowperiod=26
            //signalperiod=9
            //fastmatype=0
            //slowmatype=0
            //signalmatype=0
            //fastkperiod=5
            //slowkmatype=0
            //fastdmatype=0
            //fastdperiod=0
            //timeperiod1=7
            //timeperiod2=14
            //timeperiod3=28
            //acceleration=0.01
            //maximum=0.20

            var apiFunctions = Enum.GetValues(typeof(ApiFunctions)) as ApiFunctions[];
            var outputSize = Enum.GetValues(typeof(OutputSize)) as OutputSize[];

            var patterns = new List<string>();

            var apiFunctionsPattern = $"{ApiParametersDic.GetWord(ApiParameters.Function)}=({string.Join("|", apiFunctions.Where(q => q > 0).Select(q => q.ToString()))})";

            var symbolPattern = $"{ApiParametersDic.GetWord(ApiParameters.Symbol)}=([0-9a-zA-Z.-]" + "{3,})"; //numbers?
            patterns.Add(symbolPattern);

            var intervalsPattern = $"{ApiParametersDic.GetWord(ApiParameters.Interval)}=({string.Join("|", Intervals.Values)})";
            patterns.Add(intervalsPattern);

            var outputSizePattern = $"{ApiParametersDic.GetWord(ApiParameters.OutputSize)}=({string.Join("|", outputSize.Where(q => q > 0)).ToLower()})";
            patterns.Add(outputSizePattern);

            var apikeyPattern = $"{ApiParametersDic.GetWord(ApiParameters.ApiKey)}=(([0-9A-Z]" + "{16})|demo)";
            patterns.Add(apikeyPattern);

            var symbolsPattern = $"({ApiParametersDic.GetWord(ApiParameters.FromSymbol)}|{ApiParametersDic.GetWord(ApiParameters.ToSymbol)})=([0-9]" + "{3,5})" + "([a-zA-Z]" + "{1,5})";
            patterns.Add(symbolsPattern);

            var dataTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.DataType)}=({string.Join("|", Enum.GetNames(typeof(GettingDataType))).ToLower()})";
            patterns.Add(dataTypePattern);

            var marketPattern = $"{ApiParametersDic.GetWord(ApiParameters.Market)}=([A-Z]" + "{3,})" + "([a-z]" + "{1,})";
            patterns.Add(marketPattern);

            var keyWordsPattern = $"{ApiParametersDic.GetWord(ApiParameters.KeyWords)}=([a-zA-Z]" + "{3,})";
            patterns.Add(keyWordsPattern);

            var seriesTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.SeriesType)}=({string.Join("|", Enum.GetNames(typeof(SeriesType))).ToLower()})";
            patterns.Add(seriesTypePattern);
            
            var floatingNumbersPattern = 
                $"(" +
                $"{ApiParametersDic.GetWord(ApiParameters.SlowLimit)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.Acceleration)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.Maximum)}" +
                $")=" + "([0-9]([.,][0-9]{1,3}))";
            patterns.Add(floatingNumbersPattern);

            var integersPattern =
                $"(" +
                $"{ApiParametersDic.GetWord(ApiParameters.SignalPeriod)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.SignalPeriod)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.FastMaType)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.SlowMaType)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.SignalMaType)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.FastKPeriod)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.SlowKmaType)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.FastDmaType)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.FastDPeriod)}" +
                $"|{ApiParametersDic.GetWord(ApiParameters.TimePeriod)}([0-9]" + "{0,1})" + ")=([0-9]{1,4})";
            patterns.Add(integersPattern);

            _callPattern =
                $@"^{AlphaVantageConstants.BaseAddress.Replace(@"/", @"\/")}\?{apiFunctionsPattern}" +
                $@"(\&({string.Join('|', patterns)}))+$";

            _urlRegex = new Regex(_callPattern);
        }

        public static bool IsValid(string url)
        {
            if (!_urlRegex.IsMatch(url))
            {
                var e = new Exception(AvResources.IncorrectFunctionNameValidateError);
                e.Data.Add("URL", url);

                throw e;
            }

            return true;
        }
    }
}
