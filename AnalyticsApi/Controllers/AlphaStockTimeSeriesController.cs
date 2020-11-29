using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageDto.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnalyticsApi.Controllers
{
    [ApiController]
    [Route("api/av/TimeSeries")]
    public class AlphaStockTimeSeriesController : ControllerBase
    {
        private readonly IAlphaVantageService _alphaVantageService;

        public AlphaStockTimeSeriesController(IAlphaVantageService alphaVantageService)
        {
            _alphaVantageService = alphaVantageService;
        }

        /// <summary>
        /// Search symbols.
        /// </summary>
        /// <param name="input">Search string.</param>
        [HttpGet]
        [Route("SearchSymbol")]
        public async Task<IActionResult> SearchSymbolAsync(string input)
        {
            var result = await _alphaVantageService.SearchSymbolAsync(input);

            return Ok(new
            {
                input,
                Data = result,
            });
        }

        /// <summary>
        /// A lightweight alternative to the time series APIs, 
        /// this service returns the latest price and volume information for a security of your choice. 
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        [HttpGet]
        [Route("quoteEndpoint")]
        public async Task<IActionResult> GetQuoteEndpointAsync(string symbol)
        {
            var result = await _alphaVantageService.GetQuoteEndpointAsync(symbol);

            return Ok(new
            {
                symbol,
                Data = result,
            });
        }

        /// <summary>
        /// This API returns monthly adjusted time series 
        /// (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly adjusted close, monthly volume, monthly dividend) 
        /// of the equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for 
        /// the month(or partial month) that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        [HttpGet]
        [Route("MonthlyAdjusted")]
        public async Task<IActionResult> GetMonthlyTimeSeriesAdjustedAsync(string symbol)
        {
            var result = await _alphaVantageService.GetMonthlyTimeSeriesAdjustedAsync(symbol);

            return Ok(new
            {
                symbol,
                Data = result,
            });
        }

        /// <summary>
        /// This API returns monthly time series (last trading day of each month, monthly open, monthly high, monthly low, monthly close, monthly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the month(or partial month) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        [HttpGet]
        [Route("Monthly")]
        public async Task<IActionResult> GetMonthlyTimeSeriesAsync(string symbol)
        {
            var result = await _alphaVantageService.GetMonthlyTimeSeriesAsync(symbol);

            return Ok(new
            {
                symbol,
                Data = result,
            });
        }

        /// <summary>
        /// This API returns weekly adjusted time series
        /// (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly adjusted close, weekly volume, weekly dividend) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week (or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        [HttpGet]
        [Route("WeeklyAdjusted")]
        public async Task<IActionResult> GetWeeklyTimeSeriesAdjustedAsync(string symbol)
        {
            var result = await _alphaVantageService.GetWeeklyTimeSeriesAdjustedAsync(symbol);

            return Ok(new
            {
                symbol,
                Data = result,
            });
        }

        /// <summary>
        /// This API returns weekly time series (last trading day of each week, weekly open, weekly high, weekly low, weekly close, weekly volume) 
        /// of the global equity specified, covering 20+ years of historical data.
        /// The latest data point is the cumulative prices and volume information for the week(or partial week) 
        /// that contains the current trading day, updated realtime.
        /// </summary>
        /// <param name="symbol">Target symbol.</param>
        [HttpGet]
        [Route("Weekly")]
        public async Task<IActionResult> GetWeeklyTimeSeriesAsync(string symbol)
        {
            var result = await _alphaVantageService.GetWeeklyTimeSeriesAsync(symbol);

            return Ok(new
            {
                symbol,
                Data  = result,
            });
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
        [HttpGet]
        [Route("DailyAdjusted")]
        public async Task<IActionResult> GetDailyTimeSeriesAdjustedAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            var result = await _alphaVantageService.GetDailyTimeSeriesAdjustedAsync(symbol, outputSize);

            return Ok(new
            {
                symbol,
                outputSize = outputSize.ToString(),
                Data = result,
            });
        }

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
        [HttpGet]
        [Route("Daily")]
        public async Task<IActionResult> GetDailyTimeSeriesAsync(string symbol, OutputSize outputSize = OutputSize.Compact)
        {
            var result = await _alphaVantageService.GetDailyTimeSeriesAsync(symbol, outputSize);

            return Ok(new
            {
                symbol,
                outputSize = outputSize.ToString(),
                Data = result,
            });
        }

        /// <summary>
        /// This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified. 
        /// </summary>
        [HttpGet]
        [Route("Intraday")]
        public async Task<IActionResult> GetIntradaySeriesAsync(string symbol, Intervals interval, OutputSize outputSize = OutputSize.Full)
        {
            var result = await _alphaVantageService.GetIntradaySeriesAsync(symbol, interval, outputSize);

            return Ok(new
            {
                symbol,
                interval = interval.ToString(),
                outputSize = outputSize.ToString(),
                Data = result,
            });
        }
    }
}
