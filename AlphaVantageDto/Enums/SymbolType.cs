using System.Runtime.Serialization;

namespace AlphaVantageDto.Enums
{
    public enum SymbolType
    {
        ETF,
        Equity,
        Currency,
        [EnumMember(Value = "Mutual Fund")]
        MutualFund,
    }
}
