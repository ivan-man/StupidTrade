using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageConnector
{
    /// <summary>
    /// Implementation of https://www.alphavantage.co/documentation 
    /// </summary>
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
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="outputSize"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SampleDto>> GetIntradaySeriesAsync(string symbol, IntervalsEnum interval, OutputSize outputSize = OutputSize.Full)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (outputSize == OutputSize.None)
            {
                throw new ArgumentException(nameof(outputSize));
            }

            if (interval == IntervalsEnum.Unknown)
            {
                throw new ArgumentException(nameof(interval));
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
        public async Task<Dictionary<DateTime, SampleDto>> GetDailyTimeSeriesAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (outputSize == OutputSize.None)
            {
                throw new ArgumentException(nameof(outputSize));
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
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetDailyTimeSeriesAdjustedAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (outputSize == OutputSize.None)
            {
                throw new ArgumentException(nameof(outputSize));
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
        public async Task<Dictionary<DateTime, SampleDto>> GetWeeklyTimeSeriesAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
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
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetWeeklyTimeSeriesAdjustedAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
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
        public async Task<Dictionary<DateTime, SampleDto>> GetMonthlyTimeSeriesAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
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
        public async Task<Dictionary<DateTime, SampleAdjustedDto>> GetMonthlyTimeSeriesAdjustedAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
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
        public async Task<GlobalQuoteDto> GetQuoteEndpointAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
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
        public async Task<IEnumerable<SymbolDto>> SearchSymbolAsync(string input)
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


        #region Technical indicators

        /// <summary>
        /// Returns the simple moving average (SMA) values.
        /// </summary>
        /// <param name="symbol">
        /// The name of the security of your choice.
        /// </param>
        /// <param name="interval">
        /// Time interval between two consecutive data points in the time series
        /// </param>
        /// <param name="timePeriod">
        /// Number of data points used to calculate each moving average value. 
        /// Positive integers are accepted (e.g., time_period=60, time_period=200)
        /// </param>
        /// <param name="seriesType"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, SmaSampleDto>> GetSmaAsync(string symbol, IntervalsEnum interval, int timePeriod, SeriesType seriesType)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == IntervalsEnum.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }

            if (timePeriod < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(timePeriod));
            }

            var function = ApiFunctions.SMA;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.Interval, Intervals.Values[interval] },
                { ApiParameters.TimePeriod, timePeriod.ToString() },
                { ApiParameters.SeriesType, seriesType.ToLower() },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SmaSampleDto>>(function, parameters);
            return response.Data;
        }

        /// <summary>
        /// This API returns the exponential moving average(EMA) values.
        /// </summary>
        /// <param name="symbol">
        /// The name of the security of your choice.
        /// </param>
        /// <param name="interval">
        /// Time interval between two consecutive data points in the time series
        /// </param>
        /// <param name="timePeriod">
        /// Number of data points used to calculate each moving average value. 
        /// Positive integers are accepted (e.g., time_period=60, time_period=200)
        /// </param>
        /// <param name="seriesType"></param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, EmaSampleDto>> GetEmaAsync(string symbol, IntervalsEnum interval, int timePeriod, SeriesType seriesType)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == IntervalsEnum.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }

            if (timePeriod < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(timePeriod));
            }

            var function = ApiFunctions.EMA;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.Interval, Intervals.Values[interval] },
                { ApiParameters.TimePeriod, timePeriod.ToString() },
                { ApiParameters.SeriesType, seriesType.ToLower() },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, EmaSampleDto>>(function, parameters);
            return response.Data;
        }

        /// <summary>
        /// This API returns the volume weighted average price (VWAP) for intraday time series. 
        /// </summary>
        /// <param name="symbol">
        /// The name of the security of your choice.
        /// </param>
        /// <param name="interval">
        /// Time interval between two consecutive data points in the time series
        /// </param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, VwapSampleDto>> GetVwapAsync(string symbol, IntervalsEnum interval)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == IntervalsEnum.Unknown)
            {
                throw new ArgumentException(nameof(interval));
            }

            var function = ApiFunctions.VWAP;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.Interval, Intervals.Values[interval] },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, VwapSampleDto>>(function, parameters);
            return response.Data;
        }


        #endregion Technical indicators

        #region Sector
        /// <summary>
        /// This API returns the realtime and historical sector performances calculated from S&P500 incumbents. 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<PerfomanceRank, PerformanceDto>> GetSectorAsync()
        {
            var function = ApiFunctions.SECTOR;

            var response = await _connector.RequestApiAsync<Dictionary<PerfomanceRank, PerformanceDto>>(function, null);

            return response.Data;
        }

        #endregion Sector

        #region Forex (not implemented)
        #endregion Forex

        #region Digital & Crypto Currencies Realtime (not implemented)
        #endregion Digital & Crypto Currencies Realtime
    }
}
