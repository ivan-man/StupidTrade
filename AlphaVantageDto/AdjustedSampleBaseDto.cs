using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class InputAdjustedSampleDto : SampleDto
    {
        public float AdjustedClose { get; set; }

        public float DividendAmount { get; set; }
    }
}
