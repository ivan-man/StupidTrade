using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsApi.Controllers
{
    [ApiController]
    [Route("api/AlphaIndicator")]
    public class AlphaIndicatorsController : ControllerBase
    {
        private IAlphaVantageService _alphaVantageService;

        public AlphaIndicatorsController(IAlphaVantageService alphaVantageService)
        {
            _alphaVantageService = alphaVantageService;
        }

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
        /// <returns>List of <see cref="SmaSampleDto"/>.</returns>
        //[Authorize]
        [HttpGet]
        [Route("api/GetSma")]
        public async Task<IActionResult> GetSmaAsync(string symbol, IntervalsEnum interval, int timePeriod, SeriesType seriesType)
        {
            var result = await _alphaVantageService.GetSmaAsync(symbol, interval, timePeriod, seriesType);

            return Ok(new
            {
                symbol,
                interval = interval.ToString(),
                timePeriod,
                seriesType = seriesType.ToString(),
                Data = result
            });
        }
    }
}
