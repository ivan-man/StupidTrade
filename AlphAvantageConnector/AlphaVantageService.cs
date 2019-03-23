using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaVantageConnector
{
    public class AlphaVantageService : IAlphaVantageService
    {
        private readonly IAlphaVantageConnector _connector;

        public AlphaVantageService(IAlphaVantageConnector connector)
        {
            _connector = connector;
        }

        /// <summary>
        /// This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified. 
        /// </summary>
        public async Task<Dictionary<DateTime, SampleDto>> GetIntradaySeries(string symbol, IntervalsEnum interval, OutputSize outputSize = OutputSize.Full)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            if (outputSize == 0)
            {
                throw new ArgumentException($"Incorrect {nameof(outputSize)}.");
            }

            if (interval == 0)
            {
                throw new ArgumentException($"Incorrect {nameof(interval)}.");
            }

            var function = ApiFunctions.TIME_SERIES_INTRADAY;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.Interval, Intervals.Values[interval] },
                { ApiParameters.OutputSize, outputSize.ToLower() },
            };

            var result = await _connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(function, parameters);
            var data = result.Data;
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public async Task<IEnumerable<SymbolDto>> SearchSymbol(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(AvResources.SymbolSearchInputEmptyEx);
            }

            if (input.Length < 3) return null;

            var function = ApiFunctions.SYMBOL_SEARCH;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.KeyWords, input }
            };

            var response = await _connector.RequestApiAsync<SymbolBestMatchesDto>(function, parameters);
            return response.Data.BestMatches;
        }
    }
}
