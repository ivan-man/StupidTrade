
using System;
using System.Collections.Generic;

namespace AlphaVantageDto
{
    public class EmaSampleDto : EmaSampleAlphaDto
    {
        public DateTime Time { get; set; }

        public static explicit operator EmaSampleDto(KeyValuePair<DateTime, EmaSampleAlphaDto> pair)
        {
            return new EmaSampleDto
            {
                Time = pair.Key,
                EMA = pair.Value.EMA
            };
        }
    }
}
