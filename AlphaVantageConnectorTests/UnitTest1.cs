using AlphaVantageConnector;
using AlphaVantageConnector.Dictionaries;
using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Validation;
using AlphaVantageDto;
using AlphaVantageDto.Enums;
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
        private const string demoKey = "demo";
        private const string normalSizeKey = "0123456789ABCDEF";

        private IApiKeyService _realKeySrvice = new ApiKeyService();

        private readonly Mock<IApiKeyService> _apiKeyServiceMock = new Mock<IApiKeyService>();
        private readonly IApiKeyService _apiKeyServiceReal = new ApiKeyService();

        private readonly Mock<IApiCallValidator> _apiCallValidatorMock = new Mock<IApiCallValidator>();

        private IApiCallValidator _apiCallValidatorReal = new ApiCallValidator();
        private IRequestCompositor _requestCompositorReal;

        private IAlphaVantageConnector _connectorReal;
        private IAlphaVantageService _alphaVantageServiceReal;

        private readonly HttpClient _apiHttpClient = new HttpClient { BaseAddress = new System.Uri(@"https://www.alphavantage.co/query") };

        public UnitTest1()
        {
            Init();
        }


        private void Init()
        {
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
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("demo");
            _requestCompositorReal = new RequestCompositor(_apiCallValidatorMock.Object);

            var response = await _connectorReal.RequestApiAsync<InformationDto>(
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, "MSFT" }
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
                { ApiParameters.Symbol, "MSFT" }
            });

            var metaData = response.MetaData;

            Assert.Equal("Weekly Prices (open, high, low, close) and Volumes", metaData.Information);
            Assert.Equal("MSFT", metaData.Symbol);

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
                    { ApiParameters.Symbol, "MSFT" }
               }
            ));

            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns("0123456789ABCDEF");
            var request = _requestCompositorReal.ComposeUrl(
                _apiKeyServiceMock.Object.GetKey(),
                ApiFunctions.TIME_SERIES_WEEKLY,
                new Dictionary<ApiParameters, string>
                {
                    { ApiParameters.Symbol, "MSFT" }
                }
            );

            Assert.Equal(@"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&apikey=0123456789ABCDEF&symbol=MSFT", request.ToString());
        }
        #endregion CcoreTests


        [Fact]
        public async Task SearchSymbolTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.SearchSymbol("BabA");

            Assert.True(result?.Any() == true);
            Assert.True(result?.Any(q => q.Symbol.Equals("BABA")) == true);
        }


        [Fact]
        public async Task GetIntradaySeriesTest()
        {
            _apiKeyServiceMock.Setup(q => q.GetKey()).Returns(_realKeySrvice.GetKey());
            var result = await _alphaVantageServiceReal.GetIntradaySeries("BABA", IntervalsEnum.FiveMin);

            Assert.True(result?.Any() == true);
            //Assert.True(result?.Any() == true);
            //Assert.True(result?.Any(q => q.Symbol.Equals("BABA")) == true);
        }
    }
}
