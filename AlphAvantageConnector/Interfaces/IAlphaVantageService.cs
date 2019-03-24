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
        Task<Dictionary<DateTime, SampleDto>> GetIntradaySeries(string symbol, IntervalsEnum interval, OutputSize outputSize = OutputSize.Full);


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
        /// <returns></returns>
        /// <returns></returns>
        Task<Dictionary<DateTime, SampleDto>> GetDailyTimeSeries(string symbol, OutputSize outputSize = OutputSize.Compact);


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
        Task<Dictionary<DateTime, SampleAdjustedDto>> GetDailyTimeSeriesAdjusted(string symbol, OutputSize outputSize = OutputSize.Compact);

        /// <summary>
        /// This API returns weekly time series (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week(or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, SampleDto>> GetWeeklyTimeSeries(string symbol);

        /// <summary>
        /// This API returns weekly adjusted time series
        /// (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly adjusted close, weekly volume, weekly dividend) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week (or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, SampleAdjustedDto>> GetWeeklyTimeSeriesAdjusted(string symbol);

        /// <summary>
        /// This API returns monthly time series (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the month(or partial month) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, SampleDto>> GetMonthlyTimeSeries(string symbol);

        /// <summary>
        /// This API returns monthly adjusted time series 
        /// (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly adjusted close, monthly volume, monthly dividend) 
        /// of the equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for 
        /// the month(or partial month) that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, SampleAdjustedDto>> GetMonthlyTimeSeriesAdjusted(string symbol);

        /// <summary>
        /// A lightweight alternative to the time series APIs, 
        /// this service returns the latest price and volume information for a security of your choice. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<GlobalQuoteDto> GetQuoteEndpoint(string symbol);


        /// <summary>
        /// Search symbols.
        /// </summary>
        /// <param name="input"></param>
        Task<IEnumerable<SymbolDto>> SearchSymbol(string input);

    }
}
