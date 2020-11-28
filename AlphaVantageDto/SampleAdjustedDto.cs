using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class SampleAdjustedDto : SampleAdjustedAlphaDto
    {
        public DateTime Time { get; set; }

        public static explicit operator SampleAdjustedDto(KeyValuePair<DateTime, SampleAdjustedAlphaDto> pair)
        {
            return new SampleAdjustedDto
            {
                Time = pair.Key,
                AdjustedClose = pair.Value.AdjustedClose,
                DividendAmount = pair.Value.DividendAmount,
                Open = pair.Value.Open,
                Close = pair.Value.Close,
                High = pair.Value.High,
                Low = pair.Value.Low,
                Volume = pair.Value.Volume,
            };
        }
    }
}
