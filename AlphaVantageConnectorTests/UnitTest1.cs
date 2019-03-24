using AlphaVantageConnector;
using AlphaVantageConnector.Dictionaries;
using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Validation;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
using Common;
using Common.Interfaces;
using Moq;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AlphaVantageConnectorTests
{
    public class UnitTest1
    {
        private const string _testSymbol = "BABA";

        private const string demoKey = "demo";
        private const string normalSizeKey = "0123456789ABCDEF";


        private readonly HttpClientManager _clientManagerReal = new HttpClientManager();
        private IRateLimitHttpClient _apiHttpClient;

        private IApiKeyService _realKeySrvice = new ApiKeyService();

        private readonly Mock<IApiKeyService> _apiKeyServiceMock = new Mock<IApiKeyService>();
        private readonly IApiKeyService _apiKeyServiceReal = new ApiKeyService();

        private readonly Mock<IApiCallValidator> _apiCallValidatorMock = new Mock<IApiCallValidator>();

        private IApiCallValidator _apiCallValidatorReal = new ApiCallValidator();
        private IRequestCompositor _requestCompositorReal;

        private IAlphaVantageConnector _connectorReal;
        private IAlphaVantageService _alphaVantageServiceReal;



        public UnitTest1()
        {
            Init();
        }


        private void Init()
        {
            _apiHttpClient = _clientManagerReal.GetRateLimitClient(@"https://www.alphavantage.co/query", 10000, 7);

            _apiCallValidatorMock.Setup(q => q.Validate(It.IsAny<string>())).Returns(true);

            _apiCallValidatorReal = new ApiCallValidator();
            _requestCompositorReal = new RequestCompositor(_apiCallValidatorReal);

            _connectorReal = new AlphaVantageConnector.AlphaVantageConnector(_apiKeyServiceMock.Object, _requestCompositorReal, _apiHttpClient);

            _alphaVantageServiceReal = new AlphaVantageService(_connectorReal);

        }

        #region CcoreTests

        [Fact]
        public async Task InformationAboutFreeKeyTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(demoKey);
            _requestCompositorReal = new RequestCompositor(_apiCallValidatorMock.Object);

            var response = await _connectorReal.RequestApiAsync<InformationDto>(
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, _testSymbol }
                }
            );

            var informationDto = response.Data;

            Assert.Equal(
                "The **demo** API key is for demo purposes only. Please claim your free API key at (https://www.alphavantage.co/support/#api-key) to explore our full API offerings. It takes fewer than 20 seconds, and we are committed to making it free forever.",
                informationDto?.Information);
        }


        [Fact]
        public async Task ConnectorGetDataTest()
        {
            var connector = new AlphaVantageConnector.AlphaVantageConnector(_apiKeyServiceReal, _requestCompositorReal, _apiHttpClient);

            var response = await connector.RequestApiAsync<Dictionary<DateTime, SampleDto>>(ApiFunctions.TIME_SERIES_WEEKLY, new Dictionary<ApiParameters, string>
            {
                { ApiParameters.Symbol, _testSymbol }
            });

            var metaData = response.MetaData;

            Assert.Equal("Weekly Prices (open, high, low, close) and Volumes", metaData.Information);
            Assert.Equal(_testSymbol, metaData.Symbol);

            var data = response.Data;

            Assert.True(data.Any());
        }


        [Fact]
        public void CurrencyDescTest()
        {
            var desk = CurrencyDesc.GetSescripton(Currency.AED);
            Assert.False(string.IsNullOrEmpty(desk));
        }


        [Fact]
        public void CompositorValidatorTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("Fake key");
            var ex = Assert.Throws<Exception>(() => _requestCompositorReal.ComposeUrl(
               _apiKeyServiceMock.Object.GetKey(),
               ApiFunctions.TIME_SERIES_WEEKLY,
               new Dictionary<ApiParameters, string>
               {
                    { ApiParameters.Symbol, _testSymbol }
               }
            ));

            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("0123456789ABCDEF");
            var request = _requestCompositorReal.ComposeUrl(
                _apiKeyServiceMock.Object.GetKey(),
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, _testSymbol }
                }
            );

            Assert.Equal($@"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&apikey=0123456789ABCDEF&symbol={_testSymbol}", request.ToString());
        }
        #endregion CcoreTests

        #region StockTimeSeries

        [Fact]
        public async Task SearchSymbolTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.SearchSymbolAsync("BabA");

            Assert.True(result?.Any() == true);
            Assert.True(result?.Any(q => q.Symbol.Equals(_testSymbol)) == true);
        }

        [Fact]
        public async Task GetIntradaySeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetIntradaySeriesAsync(_testSymbol, IntervalsEnum.FiveMin);

            Assert.True(result?.Any() == true);
        }

        [Fact]
        public async Task GetDailyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetDailyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
            Assert.True(result?.Count == 100);
        }

        [Fact]
        public async Task GetDailyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetDailyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
            Assert.True(result?.Count == 100);
        }

        [Fact]
        public async Task GetWeeklyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetWeeklyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Fact]
        public async Task GetWeeklyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetWeeklyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Fact]
        public async Task GetMonthlyTimeSeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetMonthlyTimeSeriesAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Fact]
        public async Task GetMonthlyTimeSeriesAdjustedTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetMonthlyTimeSeriesAdjustedAsync(_testSymbol);

            Assert.True(result?.Any() == true);
        }

        [Fact]
        public async Task GetQuoteEndpointTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetQuoteEndpointAsync(_testSymbol);

            Assert.NotNull(result);
            Assert.Equal(_testSymbol, result.Symbol);
        }
        #endregion StockTimeSeries

        #region Technical indicators
        [Fact]
        public async Task GetSmaTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetSmaAsync(_testSymbol, IntervalsEnum.FiveMin, 60, SeriesType.Close);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        #endregion Technical indicators
    }
}
