using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class SampleAdjustedDto : SampleDto
    {
        public float AdjustedClose { get; set; }

        public float DividendAmount { get; set; }
    }
}
