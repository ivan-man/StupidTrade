using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class MetaData
    {
        public string Information { get; set; }

        public string Notes { get; set; }

        public string Symbol { get; set; }

        public DateTime LastRefreshed { get; set; }

        public OutputSize OutputSize { get; set; }

        public string TimeZone { get; set; }
    }
}
