using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <inheritdoc cref="IAlphaVantageService.GetIntradaySeriesAsync"/>
        public async Task<List<SampleDto>> GetIntradaySeriesAsync(string symbol, Enums.Intervals interval, OutputSize outputSize = OutputSize.Full)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (outputSize == OutputSize.None)
            {
                throw new ArgumentException(nameof(outputSize));
            }

            if (interval == Enums.Intervals.Unknown)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetDailyTimeSeriesAsync"/>
        public async Task<List<SampleDto>> GetDailyTimeSeriesAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetDailyTimeSeriesAdjustedAsync"/>
        public async Task<List<SampleAdjustedDto>> GetDailyTimeSeriesAdjustedAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleAdjustedDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetWeeklyTimeSeriesAsync"/>
        public async Task<List<SampleDto>> GetWeeklyTimeSeriesAsync(string symbol)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetWeeklyTimeSeriesAdjustedAsync"/>
        public async Task<List<SampleAdjustedDto>> GetWeeklyTimeSeriesAdjustedAsync(string symbol)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleAdjustedDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetMonthlyTimeSeriesAsync"/>
        public async Task<List<SampleDto>> GetMonthlyTimeSeriesAsync(string symbol)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetMonthlyTimeSeriesAdjustedAsync"/>
        public async Task<List<SampleAdjustedDto>> GetMonthlyTimeSeriesAdjustedAsync(string symbol)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SampleAdjustedAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SampleAdjustedDto) q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetQuoteEndpointAsync"/>
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

        /// <inheritdoc cref="IAlphaVantageService.SearchSymbolAsync"/>
        public async Task<List<SymbolDto>> SearchSymbolAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(AvResources.SymbolSearchInputEmptyEx);
            }

            if (input.Length < 2)
            {
                return null;
            }

            var function = ApiFunctions.SYMBOL_SEARCH;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.KeyWords, input }
            };

            var response = await _connector.RequestApiAsync<SymbolBestMatchesDto>(function, parameters);
            return response.Data.BestMatches.ToList();
        }

        #endregion StockTimeSeries


        #region Technical indicators

        /// <inheritdoc cref="IAlphaVantageService.GetSmaAsync"/>
        public async Task<List<SmaSampleDto>> GetSmaAsync(string symbol, Enums.Intervals interval, int timePeriod, SeriesType seriesType)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == Enums.Intervals.Unknown)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, SmaSampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (SmaSampleDto)q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetEmaAsync"/>
        public async Task<List<EmaSampleDto>> GetEmaAsync(string symbol, Enums.Intervals interval, int timePeriod, SeriesType seriesType)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == Enums.Intervals.Unknown)
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

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, EmaSampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (EmaSampleDto)q).ToList();
        }

        /// <inheritdoc cref="IAlphaVantageService.GetVwapAsync"/>
        public async Task<List<VwapSampleDto>> GetVwapAsync(string symbol, Enums.Intervals interval)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            if (interval == Enums.Intervals.Unknown)
            {
                throw new ArgumentException(nameof(interval));
            }

            var function = ApiFunctions.VWAP;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, symbol },
                { ApiParameters.Interval, Intervals.Values[interval] },
            };

            var response = await _connector.RequestApiAsync<Dictionary<DateTime, VwapSampleAlphaDto>>(function, parameters);
            return response.Data.Select(q => (VwapSampleDto)q).ToList();
        }

        #endregion Technical indicators


        #region Sector

        /// <inheritdoc cref="IAlphaVantageService.GetSectorAsync"/>
        public async Task<List<PerformanceDto>> GetSectorAsync()
        {
            var function = ApiFunctions.SECTOR;

            var response = await _connector.RequestApiAsync<Dictionary<PerfomanceRank, PerformanceAlphaDto>>(function, null);

            return response.Data.Select(q => (PerformanceDto)q).ToList();
        }

        #endregion Sector

        #region Forex (not implemented)
        #endregion Forex

        #region Digital & Crypto Currencies Realtime (not implemented)
        #endregion Digital & Crypto Currencies Realtime
    }
}
