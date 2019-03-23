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
    /// Not strong validation of AV API url, but API Key must be first.
    /// </summary>
    public class ApiCallValidator : IApiCallValidator
    {
        private readonly string _callPattern;
        private Regex _urlRegex;

        public ApiCallValidator()
        {
            // https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&outputsize=full&apikey=demo

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

            var apiFunctionsPattern = $"{ApiParametersDic.GetWord(ApiParameters.Function)}=({string.Join("|", apiFunctions.Select(q => q.ToString()))})";

            var symbolPattern = $"{ApiParametersDic.GetWord(ApiParameters.Symbol)}=([0-9A-Z.-]" + "{3,})"; //numbers?
            patterns.Add(symbolPattern);

            var intervalsPattern = $"{ApiParametersDic.GetWord(ApiParameters.Interval)}=({string.Join("|", Intervals.Values)})";
            patterns.Add(intervalsPattern);

            var outputSizePattern = $"{ApiParametersDic.GetWord(ApiParameters.OutputSize)}=({string.Join("|", outputSize)})";
            patterns.Add(outputSizePattern);

            var apikeyPattern = $"{ApiParametersDic.GetWord(ApiParameters.ApiKey)}=(([0-9A-Z]" + "{16})|demo)";
            patterns.Add(apikeyPattern);

            var symbolsPattern = $"({ApiParametersDic.GetWord(ApiParameters.FromSymbol)}|{ApiParametersDic.GetWord(ApiParameters.ToSymbol)})=([0-9]" + "{3})" + "([a-z]" + "{1,})";
            patterns.Add(symbolsPattern);

            var dataTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.DataType)}=({string.Join("|", Enum.GetNames(typeof(GettingDataType))).ToLower()})";
            patterns.Add(dataTypePattern);

            var marketPattern = $"{ApiParametersDic.GetWord(ApiParameters.Market)}=([A-Z]" + "{3})" + "([a-z]" + "{1,})";
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
                $"|{ApiParametersDic.GetWord(ApiParameters.TimePeriod)}([0-9])" +
                ")=([0-9]{1,4})";
            patterns.Add(integersPattern);


            //var slowLimitPattern = $"{ApiParametersDic.GetWord(ApiParameters.SlowLimit)}=" + "([0-9]([.,][0-9]{1,3}))";
            //patterns.Add(slowLimitPattern);

            //var signalPeriodPattern = $"{ApiParametersDic.GetWord(ApiParameters.SignalPeriod)}=" + "([0-9]{1,4}))";
            //patterns.Add(signalPeriodPattern);

            //var fastMaTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.FastMaType)}=" + "([0-9]{1,4}))";
            //patterns.Add(fastMaTypePattern);

            //var slowMaTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.SlowMaType)}=" + "([0-9]{1,4}))";
            //patterns.Add(slowMaTypePattern);

            //var signalMaTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.SignalMaType)}=" + "([0-9]{1,4}))";
            //patterns.Add(signalMaTypePattern);

            //var fastKPeriodPattern = $"{ApiParametersDic.GetWord(ApiParameters.FastKPeriod)}=" + "([0-9]{1,4}))";
            //patterns.Add(fastKPeriodPattern);

            //var slowKMaTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.SlowKmaType)}=" + "([0-9]{1,4}))";
            //patterns.Add(slowKMaTypePattern);

            //var fastDmaTypePattern = $"{ApiParametersDic.GetWord(ApiParameters.FastDmaType)}=" + "([0-9]{1,4}))";
            //patterns.Add(fastDmaTypePattern);

            //var fastDPeriodPattern = $"{ApiParametersDic.GetWord(ApiParameters.FastDPeriod)}=" + "([0-9]{1,4}))";
            //patterns.Add(fastDPeriodPattern);

            //var timePeriodPattern = $"{ApiParametersDic.GetWord(ApiParameters.TimePeriod)}([0-9])=" + "([0-9]{1,4}))";
            //patterns.Add(timePeriodPattern);

            //var accelerationPattern = $"{ApiParametersDic.GetWord(ApiParameters.Acceleration)}=" + "([0-9]([.,][0-9]{1,3}))";
            //patterns.Add(accelerationPattern);

            //var maximumPattern = $"{ApiParametersDic.GetWord(ApiParameters.Maximum)}=" + "([0-9]([.,][0-9]{1,3}))";
            //patterns.Add(maximumPattern);


            _callPattern =
                $@"^{AlphaVantageConstants.BaseAddress.Replace(@"/", @"\/")}\?{apiFunctionsPattern}" +
                $@"(\&({string.Join('|', patterns)}))+$";


            //_callPattern =
            //    $@"^{AlphaVantageConstants.BaseAddress.Replace(@"/", @"\/")}\?test" +
            //    $"(\&({string.Join('|', patterns)}))+$";

            _urlRegex = new Regex(_callPattern);
        }

        public bool Validate(string url)
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
