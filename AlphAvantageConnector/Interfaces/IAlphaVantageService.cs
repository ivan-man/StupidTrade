using AlphaVantageDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageConnector.Interfaces
{
    public interface IAlphaVantageService
    {
        Task<IEnumerable<SymbolDto>> SearchSymbol(string input);
    }
}
