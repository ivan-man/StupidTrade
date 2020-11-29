using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Helpers;
using AlphaVantageConnector.Resources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphaVantageConnector
{
    /// <summary>
    /// Interval values to insert into a API request
    /// </summary>
    public class Intervals : IEnumerable<string>
    {
        private Intervals()
        {
        }

        public static Intervals Values = new Intervals();
        
        public string this[Enums.Intervals value]
        {
            get
            {
                switch (value)
                {
                    case Enums.Intervals.OneMin:
                        return "1min";
                    case Enums.Intervals.FiveMin:
                        return "5min";
                    case Enums.Intervals.FifteenMin:
                        return "15min";
                    case Enums.Intervals.ThirtyMin:
                        return "30min";
                    case Enums.Intervals.SixtyMin:
                        return "60min";
                    case Enums.Intervals.Daily:
                        return "daily";
                    case Enums.Intervals.Weekly:
                        return "weekly";
                    case Enums.Intervals.Monthly:
                        return "monthly";
                    default:
                        throw new IndexOutOfRangeException(AvResources.InvalidtIntervalValueError);
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new IntervalsEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new IntervalsEnumerator(this);
        }
    }
}
