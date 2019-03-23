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
        Task<Dictionary<DateTime, SampleDto>> GetIntradaySeries(string symbol, IntervalsEnum interval, OutputSize outputSize = OutputSize.Full);

        Task<IEnumerable<SymbolDto>> SearchSymbol(string input);
    }
}
