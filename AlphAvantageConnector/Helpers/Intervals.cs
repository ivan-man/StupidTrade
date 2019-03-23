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
        
        public string this[IntervalsEnum value]
        {
            get
            {
                switch (value)
                {
                    case IntervalsEnum.OneMin:
                        return "1min";
                    case IntervalsEnum.FiveMin:
                        return "5min";
                    case IntervalsEnum.FifteenMin:
                        return "15min";
                    case IntervalsEnum.ThirtyMin:
                        return "30min";
                    case IntervalsEnum.SixtyMin:
                        return "60min";
                    case IntervalsEnum.Daily:
                        return "daily";
                    case IntervalsEnum.Weekly:
                        return "weekly";
                    case IntervalsEnum.Monthly:
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
