﻿using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
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

        /// <summary>
        /// This API returns intraday time series (timestamp, open, high, low, close, volume) of the equity specified. 
        /// </summary>
        public void GetIntradaySeries()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public async Task SearchSymbol(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(AvResources.SymbolSearchInputEmptyEx);
            }

            if (input.Length < 3) return;

            var function = ApiFunctions.SYMBOL_SEARCH;

            var parameters = new Dictionary<ApiParameters, string>
            {
                { ApiParameters.KeyWords, input}
            };

            await _connector.RequestApiAsync(function, parameters);
        }
    }
}
