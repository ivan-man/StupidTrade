using AlphaVantageDto.Enums;
using Newtonsoft.Json;
using System;

namespace AlphaVantageDto
{
    public class MetaData
    {
        public string Information { get; set; }

        public string Indicator { get; set; }

        public string Notes { get; set; }

        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        public OutputSize OutputSize { get; set; }

        [JsonProperty(PropertyName = "Series Type")]
        public SeriesType SeriesType { get; set; }

        [JsonProperty(PropertyName = "Time Period")]
        public int TimePeriod { get; set; }

        [JsonProperty(PropertyName = "Time Zone")]
        public string TimeZone { get; set; }
    }
}
