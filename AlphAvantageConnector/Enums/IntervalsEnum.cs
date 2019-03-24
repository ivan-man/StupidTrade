
namespace AlphaVantageConnector.Enums
{
    /// <summary>
    /// Time interval between two consecutive data points in the time series. 
    /// </summary>
    public enum IntervalsEnum : byte
    {
        Unknown, 

        OneMin,
        FiveMin,
        FifteenMin,
        ThirtyMin,
        SixtyMin,

        Daily,
        Weekly,
        Monthly,
    }
}
