using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlphaVantageConnector.Dictionaries
{
    /// <summary>
    /// Dictionary of words for building URL.
    /// </summary>
    public static class ApiParametersDic
    {
        private static ReadOnlyDictionary<ApiParameters, string> _words =
            new ReadOnlyDictionary<ApiParameters, string>(
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Unknown, AvResources.Unknown},

                    { ApiParameters.Function, "function"},
                    { ApiParameters.ApiKey, "apikey"},
                    { ApiParameters.Symbol, "symbol"},
                    { ApiParameters.Interval, "interval"},
                    { ApiParameters.OutputSize, "outputsize"},
                    { ApiParameters.FromSymbol, "from_symbol"},
                    { ApiParameters.ToSymbol, "to_symbol"},
                    { ApiParameters.DataType, "datatype"},
                    { ApiParameters.Market, "market"},
                    { ApiParameters.SeriesType, "series_type"},
                    { ApiParameters.SlowLimit, "slowlimit"},
                    { ApiParameters.SlowPeriod, "slowperiod"},
                    { ApiParameters.SignalPeriod, "signalperiod"},
                    { ApiParameters.FastKPeriod, "fastkperiod"},
                    { ApiParameters.FastMaType, "fastmatype"},
                    { ApiParameters.SlowMaType, "slowmatype"},
                    { ApiParameters.SignalMaType, "signalmatype"},
                    { ApiParameters.SlowKmaType, "slowkmatype"},
                    { ApiParameters.FastDmaType, "fastdmatype"},
                    { ApiParameters.FastDPeriod, "fastdperiod"},

                    { ApiParameters.TimePeriod, "timeperiod"}, //Numb

                    { ApiParameters.Acceleration, "acceleration"},
                    { ApiParameters.Maximum, "maximum"},

                    { ApiParameters.KeyWords, "keywords"},
                }
            );

        static ApiParametersDic()
        {
            if (_words.Count != Enum.GetNames(typeof(ApiParameters)).Length)
            {
                throw new IndexOutOfRangeException("Add words in API dictionary.");
            }
        }

        public static string GetWord(ApiParameters parameter)
        {
            return _words[parameter];
        }
    }
}
