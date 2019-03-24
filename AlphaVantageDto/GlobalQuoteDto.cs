using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class GlobalQuoteDto 
    {
        public string Symbol { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public int Volume { get; set; }
        public DateTime LatestTradingDay { get; set; }
        public float PreviousClose { get; set; }
        public float ChangePercent { get; set; }
    }
}
