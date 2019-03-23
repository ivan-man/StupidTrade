using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class SymbolDto
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public SymbolType Type { get; set; }
        public string Region { get; set; }
        public string MarketOpen { get; set; }
        public string MarketClose { get; set; }
        public string Timezone { get; set; }
        public Currency Currency { get; set; }
        public float MatchScore { get; set; }
    }
}
