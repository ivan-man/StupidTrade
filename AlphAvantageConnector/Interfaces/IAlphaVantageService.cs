using AlphaVantageConnector.Enums;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageConnector.Interfaces
{
    public interface IAlphaVantageService
    {
        /// <summary>
        /// This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified. 
        /// </summary>
        Task<List<SampleDto>> GetIntradaySeriesAsync(string symbol, Enums.Intervals interval, OutputSize outputSize = OutputSize.Full);


        /// <summary>
        /// This API returns daily time series(date, daily open, daily high, daily low, daily close, daily volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// 
        /// The most recent data point is the cumulative prices and volume information of the current trading day, updated realtime. 
        /// </summary>
        /// <param name="symbol">
        /// The name of the equity of your choice. For example: symbol=MSFT
        /// </param>
        /// <param name="outputSize">By default, outputsize=compact. 
        /// Strings compact and full are accepted with the following specifications: compact returns only the latest 100 data points; 
        /// full returns the full-length time series of 20+ years of historical data. 
        /// The "compact" option is recommended if you would like to reduce the data size of each API call. 
        /// </param>
        Task<List<SampleDto>> GetDailyTimeSeriesAsync(string symbol, OutputSize outputSize = OutputSize.Compact);


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
        Task<List<SampleAdjustedDto>> GetDailyTimeSeriesAdjustedAsync(string symbol, OutputSize outputSize = OutputSize.Compact);

        /// <summary>
        /// This API returns weekly time series (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week(or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        Task<List<SampleDto>> GetWeeklyTimeSeriesAsync(string symbol);

        /// <summary>
        /// This API returns weekly adjusted time series
        /// (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly adjusted close, weekly volume, weekly dividend) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week (or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        Task<List<SampleAdjustedDto>> GetWeeklyTimeSeriesAdjustedAsync(string symbol);

        /// <summary>
        /// This API returns monthly time series (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the month(or partial month) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        Task<List<SampleDto>> GetMonthlyTimeSeriesAsync(string symbol);

        /// <summary>
        /// This API returns monthly adjusted time series 
        /// (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly adjusted close, monthly volume, monthly dividend) 
        /// of the equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for 
        /// the month(or partial month) that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        Task<List<SampleAdjustedDto>> GetMonthlyTimeSeriesAdjustedAsync(string symbol);

        /// <summary>
        /// A lightweight alternative to the time series APIs, 
        /// this service returns the latest price and volume information for a security of your choice. 
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        Task<GlobalQuoteDto> GetQuoteEndpointAsync(string symbol);

        /// <summary>
        /// Search symbols.
        /// </summary>
        /// <param name="input"></param>
        Task<List<SymbolDto>> SearchSymbolAsync(string input);


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
        Task<List<SmaSampleDto>> GetSmaAsync(string symbol, Enums.Intervals interval, int timePeriod, SeriesType seriesType);

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
        Task<List<EmaSampleDto>> GetEmaAsync(string symbol, Enums.Intervals interval, int timePeriod, SeriesType seriesType);

        /// <summary>
        /// This API returns the volume weighted average price (VWAP) for intraday time series. 
        /// </summary>
        /// <param name="symbol">
        /// The name of the security of your choice.
        /// </param>
        /// <param name="interval">
        /// Time interval between two consecutive data points in the time series
        /// </param>
        Task<List<VwapSampleDto>> GetVwapAsync(string symbol, Enums.Intervals interval);

        #endregion Technical indicators

        #region Sector
        /// <summary>
        /// This API returns the realtime and historical sector performances calculated from S&P500 incumbents. 
        /// </summary>
        /// <returns></returns>
        Task<List<PerformanceDto>> GetSectorAsync();
        #endregion Sector
    }
}
