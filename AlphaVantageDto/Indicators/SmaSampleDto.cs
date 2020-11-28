
using System;
using System.Collections.Generic;

namespace AlphaVantageDto
{
    public class SmaSampleDto : SmaSampleAlphaDto
    {
        public DateTime Time { get; set; }

        public static explicit operator SmaSampleDto(KeyValuePair<DateTime, SmaSampleAlphaDto> pair)
        {
            return new SmaSampleDto
            {
                Time = pair.Key,
                SMA = pair.Value.SMA
            };
        }
    }
}
