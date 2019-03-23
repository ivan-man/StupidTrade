using AlphaVantageConnector.Enums;
using AlphaVantageDto;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaVantageConnector.Interfaces
{
    public interface IAlphaVantageConnector
    {
        Task<(MetaData MetaData, JToken Data)> RequestApiAsync(ApiFunctions function, IDictionary<ApiParameters, string> query = null);
    }
}
