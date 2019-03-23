using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaVantageConnector.Interfaces
{
    public interface IAlphaVantageService
    {
        Task SearchSymbol(string input);
    }
}
