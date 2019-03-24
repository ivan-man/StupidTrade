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

        #region StockTimeSeries
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(function, parameters);
            return response.Data;
        }


        /// <summary>
        /// This API returns daily time series(date, daily open, daily high, daily low, daily close, daily volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The most recent data point is the cumulative prices and volume information of the current trading day, updated realtime. 
        /// </summary>
        /// <param name="symbol">
        /// The name of the equity of your choice. For example: symbol=MSFT
        /// </param>
        /// <param name="outputSize">
        /// By default, outputsize=compact. 
        /// Strings compact and full are accepted with the following specifications: compact returns only the latest 100 data points; 
        /// full returns the full-length time series of 20+ years of historical data. 
        /// The "compact" option is recommended if you would like to reduce the data size of each API call. 
        /// </param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleDto>> GetDailyTimeSeries(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            if (outputSize == 0)
            {
                throw new ArgumentException($"Incorrect {nameof(outputSize)}.");
            }

            var function = ApiFunctions.TIME_SERIES_DAILY;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.OutputSize, outputSize.ToLower() },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(function, parameters);
            return response.Data;
        }

        /// <summary>
        /// This API returns daily time series
        /// (date, daily open, daily high, daily low, daily close, daily volume, daily adjusted close, and split/dividend events) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The most recent data point is the cumulative prices and volume information of the current trading day, updated realtime. 
        /// </summary>
        /// <param name="symbol">
        /// The name of the equity of your choice. For example: symbol=MSFT
        /// </param>
        /// <param name="outputSize">
        /// By default, outputsize=compact. 
        /// Strings compact and full are accepted with the following specifications: compact returns only the latest 100 data points; 
        /// full returns the full-length time series of 20+ years of historical data. 
        /// The "compact" option is recommended if you would like to reduce the data size of each API call. 
        /// </param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetDailyTimeSeriesAdjusted(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            if (outputSize == 0)
            {
                throw new ArgumentException($"Incorrect {nameof(outputSize)}.");
            }

            var function = ApiFunctions.TIME_SERIES_DAILY_ADJUSTED;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.OutputSize, outputSize.ToLower() },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedDto>>(function, parameters);
            return response.Data;
        }


        /// <summary>
        /// This API returns weekly time series (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week(or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleDto>> GetWeeklyTimeSeries(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            var function = ApiFunctions.TIME_SERIES_WEEKLY;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(function, parameters);
            return response.Data;
        }


        /// <summary>
        /// This API returns weekly adjusted time series
        /// (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly adjusted close, weekly volume, weekly dividend) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week (or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetWeeklyTimeSeriesAdjusted(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            var function = ApiFunctions.TIME_SERIES_WEEKLY_ADJUSTED;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedDto>>(function, parameters);
            return response.Data;
        }


        /// <summary>
        /// This API returns monthly time series (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the month(or partial month) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleDto>> GetMonthlyTimeSeries(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            var function = ApiFunctions.TIME_SERIES_MONTHLY;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(function, parameters);
            return response.Data;
        }

        /// <summary>
        /// This API returns monthly adjusted time series 
        /// (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly adjusted close, monthly volume, monthly dividend) 
        /// of the equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for 
        /// the month(or partial month) that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetMonthlyTimeSeriesAdjusted(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            var function = ApiFunctions.TIME_SERIES_MONTHLY_ADJUSTED;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedDto>>(function, parameters);
            return response.Data;
        }



        /// <summary>
        /// A lightweight alternative to the time series APIs, 
        /// this service returns the latest price and volume information for a security of your choice. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<GlobalQuoteDto> GetQuoteEndpoint(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"Empty {nameof(symbol)}.");
            }

            var function = ApiFunctions.GLOBAL_QUOTE;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol }
            };

            var response = await _connector.RequestApiAsync<GlobalQuoteWrappingDto>(function, parameters);
            return response.Data.GlobalQuote;
        }

        /// <summary>
        /// Search symbols.
        /// </summary>
        /// <param name="input"></param>
        public async Task<IEnumerable<SymbolDto>> SearchSymbol(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(AvResources.SymbolSearchInputEmptyEx);
            }

            if (input.Length < 2) return null;

            var function = ApiFunctions.SYMBOL_SEARCH;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.KeyWords, input }
            };

            var response = await _connector.RequestApiAsync<SymbolBestMatchesDto>(function, parameters);
            return response.Data.BestMatches;
        }

        #endregion StockTimeSeries

        #region Forex

        #endregion Forex
    }
}
