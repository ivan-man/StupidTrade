using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class InputAdjustedSample : InputSample
    {
        public float AdjustedClose { get; set; }

        public float DividendAmount { get; set; }
    }
}
