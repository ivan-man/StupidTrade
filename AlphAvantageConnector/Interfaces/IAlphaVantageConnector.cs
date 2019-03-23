using AlphaVantageConnector.Enums;
using AlphaVantageDto;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageConnector.Interfaces
{
    public interface IAlphaVantageConnector
    {
        Task<(MetaData MetaData, TData Data)> RequestApiAsync<TData>(ApiFunctions function, IDictionary<ApiParameters, string> query = null);
    }
}
