using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class SampleDto : SampleAlphaDto
    {
        public DateTime Time { get; set; }

        public static explicit operator SampleDto(KeyValuePair<DateTime, SampleAlphaDto> pair)
        {
            return new SampleDto
            {
                Time = pair.Key,
                Open = pair.Value.Open,
                Close = pair.Value.Close,
                High = pair.Value.High,
                Low = pair.Value.Low,
                Volume = pair.Value.Volume,
            };
        }
    }
}
