using AlphaVantageConnector;
using AlphaVantageConnector.Dictionaries;
using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Common;
using Common.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaVantageConnectorTests
{
    public class UnitTest1
    {
        private const string _testSymbol = "BABA";

        private const string demoKey = "demo";
        private const string normalSizeKey = "0123456789ABCDEF";


        private IRateLimitHttpClient _apiHttpClient;

        private IApiKeyService _realKeySrvice = new ApiKeyService();

        private readonly Mock<IApiKeyService> _apiKeyServiceMock = new Mock<IApiKeyService>();
        private readonly IApiKeyService _apiKeyServiceReal = new ApiKeyService();

        private IAlphaVantageConnector _connectorReal;
        private IAlphaVantageService _alphaVantageServiceReal;


        [SetUp]
        public void SetUp()
        {
            var interval = 1000; //if you run all tests, set it 10000+ miliseconds, or use several keys
            _apiHttpClient = HttpClientManager.GetRateLimitClient(@"https://www.alphavantage.co/query", interval, 7);

            _connectorReal = new AlphaVantageConnector.AlphaVantageConnector(_apiKeyServiceMock.Object, _apiHttpClient);

            _alphaVantageServiceReal = new AlphaVantageService(_connectorReal);
        }

        #region CoreTests

        [Test]
        public async Task InformationAboutFreeKeyTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(demoKey);
            //_requestCompositorReal = new RequestCompositor(_apiCallValidatorMock.Object);

            var response = await _connectorReal.RequestApiAsync<InformationDto>(
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, _testSymbol }
                }
            );

            var informationDto = response.Data;

            var demoMessage = "The **demo** API key is for demo purposes only. Please claim your free API key at (https://www.alphavantage.co/support/#api-key) to explore our full API offerings. It takes fewer than 20 seconds";

            Assert.True(informationDto?.Information.Contains(demoMessage));
        }


        [Test]
        public async Task ConnectorGetDataTest()
        {
            var connector = new AlphaVantageConnector.AlphaVantageConnector(_apiKeyServiceReal, _apiHttpClient);

            var response = await connector.RequestApiAsync<Dictionary<DateTime, SampleAlphaDto>>(ApiFunctions.TIME_SERIES_WEEKLY, new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, _testSymbol }
            });

            var metaData = response.MetaData;

            Assert.AreEqual("Weekly Prices (open, high, low, close) and Volumes", metaData.Information);
            Assert.AreEqual(_testSymbol, metaData.Symbol);

            var data = response.Data;

            Assert.True(data.Any());
        }


        [Test]
        public void CurrencyDescTest()
        {
            var desk = CurrencyDesc.GetSescripton(Currency.AED);
            Assert.False(string.IsNullOrEmpty(desk));
        }

        [Test]
        public void CompositorValidatorExceptionTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("Fake key");

            var ex = Assert.Throws<Exception>(() => AlphaVantageRequestCompositor.ComposeUrl(
               _apiKeyServiceMock.Object.GetKey(),
               ApiFunctions.TIME_SERIES_WEEKLY,
               new Dictionary<ApiParameters, string>
               {
                    { ApiParameters.Symbol, _testSymbol }
               }
            ));

            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("0123456789ABCDEF");

            var request = AlphaVantageRequestCompositor.ComposeUrl(
                _apiKeyServiceMock.Object.GetKey(),
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, _testSymbol }
                }
            );

            Assert.AreEqual($@"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&apikey=0123456789ABCDEF&symbol={_testSymbol}", request.ToString());
        }

        [Test]
        public void CompositorBuildUrlTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("Fake key");

            var ex = Assert.Throws<Exception>(() => AlphaVantageRequestCompositor.ComposeUrl(
               _apiKeyServiceMock.Object.GetKey(),
               ApiFunctions.TIME_SERIES_WEEKLY,
               new Dictionary<ApiParameters, string>
               {
                    { ApiParameters.Symbol, _testSymbol }
               }
            ));

            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("0123456789ABCDEF");

            var request = AlphaVantageRequestCompositor.ComposeUrl(
                _apiKeyServiceMock.Object.GetKey(),
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, _testSymbol }
                }
            );

            Assert.AreEqual($@"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&apikey=0123456789ABCDEF&symbol={_testSymbol}", request.ToString());
        }

        #endregion CcoreTests

        #region StockTimeSeries

        [Test]
        public async Task SearchSymbolTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.SearchSymbolAsync("BabA");

            Assert.True(result?.Any() == true);
            Assert.True(result?.Any(q => q.Symbol.Equals(_testSymbol)) == true);
        }

        [Test]
        public async Task GetIntradaySeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetIntradaySeriesAsync(_testSymbol, AlphaVantageConnector.Enums.Intervals.FiveMin);

            Assert.True(result?.Any() == true);
        }

        [Test]
        public async Task GetDailyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetDailyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
            Assert.True(result?.Count == 100);
        }

        [Test]
        public async Task GetDailyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetDailyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
            Assert.True(result?.Count == 100);
        }

        [Test]
        public async Task GetWeeklyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetWeeklyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Test]
        public async Task GetWeeklyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetWeeklyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Test]
        public async Task GetMonthlyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetMonthlyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Test]
        public async Task GetMonthlyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetMonthlyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Test]
        public async Task GetQuoteEndpointTest()
        {
            var key = _realKeySrvice.GetKey();
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(key);
            var result = await _alphaVantageServiceReal.GetQuoteEndpointAsync(_testSymbol);

            Assert.NotNull(result);
            Assert.AreEqual(_testSymbol, result.Symbol);
        }
        #endregion StockTimeSeries

        #region Technical indicators
        [Test]
        public async Task GetSmaTest()
        {
            var key = _realKeySrvice.GetKey();
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(key);
            var result = await _alphaVantageServiceReal.GetSmaAsync(_testSymbol, AlphaVantageConnector.Enums.Intervals.FiveMin, 60, SeriesType.Close);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Test]
        public async Task GetEmaTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetEmaAsync(_testSymbol, AlphaVantageConnector.Enums.Intervals.FiveMin, 60, SeriesType.Close);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Test]
        public async Task GetVwapTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetVwapAsync(_testSymbol, AlphaVantageConnector.Enums.Intervals.FiveMin);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Test]
        public async Task GetSectorTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetSectorAsync();

            Assert.NotNull(result);
            Assert.True(result.Any() && result.Count == Enum.GetValues(typeof(PerfomanceRank)).Length);
        }

        #endregion Technical indicators
    }
}