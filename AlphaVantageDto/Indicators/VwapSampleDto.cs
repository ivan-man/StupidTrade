using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class VwapSampleDto : VwapSampleAlphaDto
    {
        public DateTime Time { get; set; }

        public static explicit operator VwapSampleDto(KeyValuePair<DateTime, VwapSampleAlphaDto> pair)
        {
            return new VwapSampleDto
            {
                Time = pair.Key,
                VWAP = pair.Value.VWAP
            };
        }
    }
}
